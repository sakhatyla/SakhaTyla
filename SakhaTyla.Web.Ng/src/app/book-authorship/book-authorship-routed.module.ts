import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { BookAuthorshipsComponent } from './book-authorships.component';
import { BookAuthorshipViewComponent } from './book-authorship-view.component';
import { BookAuthorshipModule } from '../book-authorship/book-authorship.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: BookAuthorshipsComponent },
      { path: ':id', component: BookAuthorshipViewComponent }
    ]),
    BookAuthorshipModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class BookAuthorshipRoutedModule {

}
