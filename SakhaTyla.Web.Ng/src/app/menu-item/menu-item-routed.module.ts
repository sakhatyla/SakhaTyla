import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { MenuItemsComponent } from './menu-items.component';
import { MenuItemViewComponent } from './menu-item-view.component';
import { MenuItemModule } from '../menu-item/menu-item.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: MenuItemsComponent },
      { path: ':id', component: MenuItemViewComponent }
    ]),
    MenuItemModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class MenuItemRoutedModule {

}
