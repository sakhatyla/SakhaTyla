import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { BookPagesComponent } from './book-pages.component';
import { BookPageViewComponent } from './book-page-view.component';
import { BookPageModule } from '../book-page/book-page.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: BookPagesComponent },
      { path: ':id', component: BookPageViewComponent }
    ]),
    BookPageModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class BookPageRoutedModule {

}
