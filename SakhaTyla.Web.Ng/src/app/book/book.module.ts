﻿import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { BookCoreModule } from '../book-core/book-core.module';
import { BookPageModule } from '../book-page/book-page.module';

import { BookListComponent } from './book-list.component';
import { BookEditComponent } from './book-edit.component';
import { BookViewComponent } from './book-view.component';
import { BooksComponent } from './books.component';
import { BookLabelModule } from '../book-label/book-label.module';

@NgModule({
  declarations: [
    BookListComponent,
    BookEditComponent,
    BookViewComponent,
    BooksComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    BookCoreModule,
    BookPageModule,
    BookLabelModule,
  ],
  exports: [
    BookListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    BookEditComponent
  ]
})
export class BookModule {

}