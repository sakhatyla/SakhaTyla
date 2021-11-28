import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { from, Observable, throwError } from 'rxjs';
import { catchError, mergeMap, switchMap } from 'rxjs/operators';
import { AuthenticationResultStatus, AuthorizeService } from './authorize.service';
import { ApplicationPaths, QueryParameterNames } from './api-authorization.constants';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeInterceptor implements HttpInterceptor {
  constructor(private authorize: AuthorizeService,
              private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.url.startsWith('assets')) {
      return next.handle(req);
    }
    return this.authorize.getAccessToken()
      .pipe(mergeMap(token => this.processRequestWithToken(token, req, next)));
  }

  private addToken(token: string, req: HttpRequest<any>): HttpRequest<any> {
    return req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }

  // Checks if there is an access_token available in the authorize service
  // and adds it to the request in case it's targeted at the same origin as the
  // single page application.
  private processRequestWithToken(token: string, req: HttpRequest<any>, next: HttpHandler) {
    if (!!token/* && this.isSameOriginUrl(req)*/) {
      req = this.addToken(token, req);
    }

    return next.handle(req).pipe(catchError(err => {
      if (err instanceof HttpErrorResponse) {
        const httpError = err as HttpErrorResponse;
        if (httpError.status === 401) {
          const returnUrl = this.router.routerState.snapshot.url;
          return from(this.login(returnUrl))
            .pipe(switchMap(success => {
              if (success) {
                return this.authorize.getAccessToken()
                  .pipe(mergeMap(newToken => {
                    return next.handle(this.addToken(newToken, req));
                  }));
              }
              return throwError(err);
            }));
        }
      }
      return throwError(err);
    }));
  }

  private async login(returnUrl: string): Promise<boolean> {
    const result = await this.authorize.signIn({ returnUrl });
    switch (result.status) {
      case AuthenticationResultStatus.Redirect:
        break;
      case AuthenticationResultStatus.Success:
        return true;
      case AuthenticationResultStatus.Fail:
        await this.router.navigate(ApplicationPaths.LoginFailedPathComponents, {
          queryParams: { [QueryParameterNames.Message]: result.message }
        });
        break;
      default:
        throw new Error(`Invalid status result ${(result as any).status}.`);
    }
    return false;
  }

  private isSameOriginUrl(req: any) {
    // It's an absolute url with the same origin.
    if (req.url.startsWith(`${window.location.origin}/`)) {
      return true;
    }

    // It's a protocol relative url with the same origin.
    // For example: //www.example.com/api/Products
    if (req.url.startsWith(`//${window.location.host}/`)) {
      return true;
    }

    // It's a relative url like /api/Products
    if (/^\/[^\/].*/.test(req.url)) {
      return true;
    }

    // It's an absolute or protocol relative url that
    // doesn't have the same origin.
    return false;
  }
}
