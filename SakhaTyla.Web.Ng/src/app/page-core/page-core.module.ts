import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { PageService } from './page.service';
import { PageSelectComponent } from './page-select.component';
import { PageShowComponent } from './page-show.component';

@NgModule({
  declarations: [
    PageSelectComponent,
    PageShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    PageService
  ],
  exports: [
    PageSelectComponent,
    PageShowComponent
  ]
})
export class PageCoreModule {

}
