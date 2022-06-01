import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { MenuItemService } from './menu-item.service';
import { MenuItemSelectComponent } from './menu-item-select.component';
import { MenuItemShowComponent } from './menu-item-show.component';

@NgModule({
  declarations: [
    MenuItemSelectComponent,
    MenuItemShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    MenuItemService
  ],
  exports: [
    MenuItemSelectComponent,
    MenuItemShowComponent
  ]
})
export class MenuItemCoreModule {

}
