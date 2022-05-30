import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { RouteEditComponent } from './route-edit.component';
import { RouteShowComponent } from './route-show.component';

@NgModule({
  declarations: [
    RouteEditComponent,
    RouteShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
  ],
  exports: [
    RouteEditComponent,
    RouteShowComponent
  ]
})
export class RouteCoreModule {

}
