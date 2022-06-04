import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { BookCoreModule } from '../book-core/book-core.module';
import { BookPageModule } from '../book-page/book-page.module';
import { BookLabelModule } from '../book-label/book-label.module';
import { BookAuthorshipModule } from '../book-authorship/book-authorship.module';

import { BookListComponent } from './book-list.component';
import { BookEditComponent } from './book-edit.component';
import { BookViewComponent } from './book-view.component';
import { BooksComponent } from './books.component';

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
    BookAuthorshipModule,
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
