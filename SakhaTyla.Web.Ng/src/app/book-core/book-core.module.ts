import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { BookService } from './book.service';
import { BookSelectComponent } from './book-select.component';
import { BookShowComponent } from './book-show.component';

@NgModule({
  declarations: [
    BookSelectComponent,
    BookShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    BookService
  ],
  exports: [
    BookSelectComponent,
    BookShowComponent
  ]
})
export class BookCoreModule {

}
