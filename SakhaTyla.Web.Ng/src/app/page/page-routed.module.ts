import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { PagesComponent } from './pages.component';
import { PageViewComponent } from './page-view.component';
import { PageModule } from '../page/page.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: PagesComponent },
      { path: ':id', component: PageViewComponent }
    ]),
    PageModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class PageRoutedModule {

}
