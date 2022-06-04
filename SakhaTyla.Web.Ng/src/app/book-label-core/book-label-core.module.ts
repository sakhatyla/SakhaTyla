import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { BookLabelService } from './book-label.service';
import { BookLabelSelectComponent } from './book-label-select.component';
import { BookLabelShowComponent } from './book-label-show.component';

@NgModule({
  declarations: [
    BookLabelSelectComponent,
    BookLabelShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    BookLabelService
  ],
  exports: [
    BookLabelSelectComponent,
    BookLabelShowComponent
  ]
})
export class BookLabelCoreModule {

}
