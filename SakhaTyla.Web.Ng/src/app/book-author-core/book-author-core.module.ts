import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { BookAuthorService } from './book-author.service';
import { BookAuthorSelectComponent } from './book-author-select.component';
import { BookAuthorShowComponent } from './book-author-show.component';

@NgModule({
  declarations: [
    BookAuthorSelectComponent,
    BookAuthorShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    BookAuthorService
  ],
  exports: [
    BookAuthorSelectComponent,
    BookAuthorShowComponent
  ]
})
export class BookAuthorCoreModule {

}
