import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { PageTypeSelectComponent } from './page-type-select.component';
import { PageTypeViewComponent } from './page-type-view.component';

@NgModule({
  declarations: [
    PageTypeSelectComponent,
    PageTypeViewComponent
  ],
  imports: [
    CoreModule
  ],
  providers: [
  ],
  exports: [
    PageTypeSelectComponent,
    PageTypeViewComponent
  ]
})
export class PageTypeModule {

}
