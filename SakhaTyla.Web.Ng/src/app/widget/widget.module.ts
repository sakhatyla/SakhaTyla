import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { WidgetCoreModule } from '../widget-core/widget-core.module';
import { WidgetTypeModule } from '../widget-type/widget-type.module';

import { WidgetListComponent } from './widget-list.component';
import { WidgetEditComponent } from './widget-edit.component';
import { WidgetViewComponent } from './widget-view.component';
import { WidgetsComponent } from './widgets.component';

@NgModule({
  declarations: [
    WidgetListComponent,
    WidgetEditComponent,
    WidgetViewComponent,
    WidgetsComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    WidgetTypeModule,
    WidgetCoreModule
  ],
  exports: [
    WidgetListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    WidgetEditComponent
  ]
})
export class WidgetModule {

}
