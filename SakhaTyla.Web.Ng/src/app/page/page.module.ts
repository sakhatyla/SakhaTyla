import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { PageCoreModule } from '../page-core/page-core.module';
import { FileCoreModule } from '../file-core/file-core.module';
import { PageTypeModule } from '../page-type/page-type.module';
import { RouteCoreModule } from '../route-core/route-core.module';
import { CommentModule } from '../comment/comment.module';

import { PageListComponent } from './page-list.component';
import { PageEditComponent } from './page-edit.component';
import { PageViewComponent } from './page-view.component';
import { PagesComponent } from './pages.component';

@NgModule({
  declarations: [
    PageListComponent,
    PageEditComponent,
    PageViewComponent,
    PagesComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    FileCoreModule,
    PageTypeModule,
    PageCoreModule,
    RouteCoreModule,
    CommentModule,
  ],
  exports: [
    PageListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    PageEditComponent
  ]
})
export class PageModule {

}
