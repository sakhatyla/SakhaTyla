import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { BookPageService } from './book-page.service';
import { BookPageSelectComponent } from './book-page-select.component';
import { BookPageShowComponent } from './book-page-show.component';

@NgModule({
  declarations: [
    BookPageSelectComponent,
    BookPageShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    BookPageService
  ],
  exports: [
    BookPageSelectComponent,
    BookPageShowComponent
  ]
})
export class BookPageCoreModule {

}
