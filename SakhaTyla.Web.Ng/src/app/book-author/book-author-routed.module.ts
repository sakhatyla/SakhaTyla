import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { BookAuthorsComponent } from './book-authors.component';
import { BookAuthorViewComponent } from './book-author-view.component';
import { BookAuthorModule } from '../book-author/book-author.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: BookAuthorsComponent },
      { path: ':id', component: BookAuthorViewComponent }
    ]),
    BookAuthorModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class BookAuthorRoutedModule {

}
