import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { MenuService } from './menu.service';
import { MenuSelectComponent } from './menu-select.component';
import { MenuShowComponent } from './menu-show.component';

@NgModule({
  declarations: [
    MenuSelectComponent,
    MenuShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    MenuService
  ],
  exports: [
    MenuSelectComponent,
    MenuShowComponent
  ]
})
export class MenuCoreModule {

}
