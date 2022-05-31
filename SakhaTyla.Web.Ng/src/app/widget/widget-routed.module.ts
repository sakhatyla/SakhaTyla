import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { WidgetsComponent } from './widgets.component';
import { WidgetViewComponent } from './widget-view.component';
import { WidgetModule } from '../widget/widget.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: WidgetsComponent },
      { path: ':id', component: WidgetViewComponent }
    ]),
    WidgetModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class WidgetRoutedModule {

}
