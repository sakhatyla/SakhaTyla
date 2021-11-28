import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';
import { MatSidenav } from '@angular/material/sidenav';
import { MediaObserver } from '@angular/flex-layout';
import { Observable } from 'rxjs';
import { debounceTime, map, tap, first } from 'rxjs/operators';

import { LoadingService } from './core/loading.service';
import { AuthorizeService } from '../api-authorization/authorize.service';
import { ConfigService } from './config/config.service';
import { ApplicationPaths } from '../api-authorization/api-authorization.constants';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'app';
  isLoading = false;
  isAuthenticated: Observable<boolean>;
  userName: Observable<string>;
  accountUrl: string;

  isHandset$: Observable<boolean> = this.media.asObservable().pipe(
    map(
      () =>
        this.media.isActive('xs') ||
        this.media.isActive('sm') ||
        this.media.isActive('lt-md')
    ),
    tap(() => this.cdRef.detectChanges()));

  constructor(private authorizeService: AuthorizeService,
              private loadingService: LoadingService,
              private configService: ConfigService,
              private cdRef: ChangeDetectorRef,
              private media: MediaObserver) {
    loadingService
      .onLoadingChanged
      .pipe(debounceTime(500))
      .subscribe((isLoading: boolean) => {
        this.isLoading = isLoading;
        this.cdRef.detectChanges();
      });
    this.accountUrl = this.configService.config.apiBaseUrl + ApplicationPaths.IdentityManagePath;
  }

  ngOnInit(): void {
    this.emitEventResize();
    this.isAuthenticated = this.authorizeService.isAuthenticated();
    this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
  }

  toggle(sidenav: MatSidenav): void {
    this.emitEventResize();
    this.isHandset$.pipe(
      first()
    ).subscribe(isHandset => {
      if (isHandset) {
        sidenav.toggle();
      }
    });
  }

  emitEventResize() {
    // fix for mat-sidenav-content not resizing
    window.dispatchEvent(new Event('resize'));
  }
}
