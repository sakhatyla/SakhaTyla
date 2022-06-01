import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { MenuItemCoreModule } from '../menu-item-core/menu-item-core.module';
import { MenuCoreModule } from '../menu-core/menu-core.module';

import { MenuItemListComponent } from './menu-item-list.component';
import { MenuItemEditComponent } from './menu-item-edit.component';
import { MenuItemViewComponent } from './menu-item-view.component';
import { MenuItemsComponent } from './menu-items.component';

@NgModule({
  declarations: [
    MenuItemListComponent,
    MenuItemEditComponent,
    MenuItemViewComponent,
    MenuItemsComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    MenuCoreModule,
    MenuItemCoreModule
  ],
  exports: [
    MenuItemListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    MenuItemEditComponent
  ]
})
export class MenuItemModule {

}
