import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { BookLabelsComponent } from './book-labels.component';
import { BookLabelViewComponent } from './book-label-view.component';
import { BookLabelModule } from '../book-label/book-label.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: BookLabelsComponent },
      { path: ':id', component: BookLabelViewComponent }
    ]),
    BookLabelModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class BookLabelRoutedModule {

}
