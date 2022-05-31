import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { WidgetService } from './widget.service';
import { WidgetSelectComponent } from './widget-select.component';
import { WidgetShowComponent } from './widget-show.component';

@NgModule({
  declarations: [
    WidgetSelectComponent,
    WidgetShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    WidgetService
  ],
  exports: [
    WidgetSelectComponent,
    WidgetShowComponent
  ]
})
export class WidgetCoreModule {

}
