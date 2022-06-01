import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { BooksComponent } from './books.component';
import { BookViewComponent } from './book-view.component';
import { BookModule } from '../book/book.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: BooksComponent },
      { path: ':id', component: BookViewComponent }
    ]),
    BookModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class BookRoutedModule {

}
