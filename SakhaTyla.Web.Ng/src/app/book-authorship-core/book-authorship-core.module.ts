import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { BookAuthorshipService } from './book-authorship.service';
import { BookAuthorshipSelectComponent } from './book-authorship-select.component';
import { BookAuthorshipShowComponent } from './book-authorship-show.component';

@NgModule({
  declarations: [
    BookAuthorshipSelectComponent,
    BookAuthorshipShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    BookAuthorshipService
  ],
  exports: [
    BookAuthorshipSelectComponent,
    BookAuthorshipShowComponent
  ]
})
export class BookAuthorshipCoreModule {

}
