import { EventEmitter, Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { finalize } from 'rxjs/operators';

import { LoadingService } from './loading.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

    constructor(private loadingService: LoadingService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // emit onStarted event before request execution
        this.loadingService.onStarted(req);

        return next
            .handle(req)
            // emit onFinished event after request execution
            .pipe(finalize(() => this.loadingService.onFinished(req)));
    }
}
