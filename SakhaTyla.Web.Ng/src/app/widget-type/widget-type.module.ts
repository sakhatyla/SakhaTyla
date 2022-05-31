import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { WidgetTypeSelectComponent } from './widget-type-select.component';
import { WidgetTypeViewComponent } from './widget-type-view.component';

@NgModule({
  declarations: [
    WidgetTypeSelectComponent,
    WidgetTypeViewComponent
  ],
  imports: [
    CoreModule
  ],
  providers: [
  ],
  exports: [
    WidgetTypeSelectComponent,
    WidgetTypeViewComponent
  ]
})
export class WidgetTypeModule {

}
