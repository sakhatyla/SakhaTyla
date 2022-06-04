import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { BookLabelCoreModule } from '../book-label-core/book-label-core.module';
import { BookCoreModule } from '../book-core/book-core.module';
import { BookPageCoreModule } from '../book-page-core/book-page-core.module';

import { BookLabelListComponent } from './book-label-list.component';
import { BookLabelEditComponent } from './book-label-edit.component';
import { BookLabelViewComponent } from './book-label-view.component';
import { BookLabelsComponent } from './book-labels.component';

@NgModule({
  declarations: [
    BookLabelListComponent,
    BookLabelEditComponent,
    BookLabelViewComponent,
    BookLabelsComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    BookCoreModule,
    BookPageCoreModule,
    BookLabelCoreModule
  ],
  exports: [
    BookLabelListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    BookLabelEditComponent
  ]
})
export class BookLabelModule {

}
