import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { BookPageCoreModule } from '../book-page-core/book-page-core.module';
import { BookCoreModule } from '../book-core/book-core.module';

import { BookPageListComponent } from './book-page-list.component';
import { BookPageEditComponent } from './book-page-edit.component';
import { BookPageViewComponent } from './book-page-view.component';
import { BookPagesComponent } from './book-pages.component';

@NgModule({
  declarations: [
    BookPageListComponent,
    BookPageEditComponent,
    BookPageViewComponent,
    BookPagesComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    BookCoreModule,
    BookPageCoreModule
  ],
  exports: [
    BookPageListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    BookPageEditComponent
  ]
})
export class BookPageModule {

}
