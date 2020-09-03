import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Route } from '@angular/router';
import { MatPaginatorIntl } from '@angular/material/paginator';

import { MaterialModule } from './material.module';
import { ApiAuthorizationModule } from '../api-authorization/api-authorization.module';
import { CoreModule } from './core/core.module';

import { AuthorizeInterceptor } from '../api-authorization/authorize.interceptor';
import { ConfigService } from './config/config.service';
import { MenuService } from './nav-menu/menu.service';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';
import { MatPaginatorIntlCustom } from './mat-paginator-intl';
import { TranslocoRootModule } from './transloco-root.module';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent
    ],
    imports: [
        BrowserAnimationsModule,
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full', canActivate: [AuthorizeGuard] },
// ADD ROUTES HERE
            {
                path: 'article',
                canActivate: [AuthorizeGuard],
                loadChildren: () => import('./article/article-routed.module').then(m => m.ArticleRoutedModule)
            },
            {
                path: 'category',
                canActivate: [AuthorizeGuard],
                loadChildren: () => import('./category/category-routed.module').then(m => m.CategoryRoutedModule)
            },
            {
                path: 'language',
                canActivate: [AuthorizeGuard],
                loadChildren: () => import('./language/language-routed.module').then(m => m.LanguageRoutedModule)
            },
            {
                path: 'file',
                canActivate: [AuthorizeGuard],
                loadChildren: () => import('./file/file-routed.module').then(m => m.FileRoutedModule)
            },
            {
                path: 'file-group',
                canActivate: [AuthorizeGuard],
                loadChildren: () => import('./file-group/file-group-routed.module').then(m => m.FileGroupRoutedModule)
            },
            {
                path: 'profile',
                canActivate: [AuthorizeGuard],
                loadChildren: () => import('./profile/profile.module').then(m => m.ProfileModule)
            },
            {
                path: 'role',
                canActivate: [AuthorizeGuard],
                loadChildren: () => import('./role/role-routed.module').then(m => m.RoleRoutedModule)
            },
            {
                path: 'user',
                canActivate: [AuthorizeGuard],
                loadChildren: () => import('./user/user-routed.module').then(m => m.UserRoutedModule)
            },
        ]),
        MaterialModule,
        CoreModule,
        ApiAuthorizationModule,
        TranslocoRootModule,
    ],
    exports: [
        MaterialModule
    ],
    providers: [
        ConfigService,
        MenuService,
        {
            provide: APP_INITIALIZER,
            useFactory: (configService: ConfigService) => {
                return () => configService.load();
            },
            multi: true,
            deps: [ConfigService]
        },
        { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
        { provide: MatPaginatorIntl, useClass: MatPaginatorIntlCustom}
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
