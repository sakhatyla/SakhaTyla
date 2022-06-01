import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { MenusComponent } from './menus.component';
import { MenuViewComponent } from './menu-view.component';
import { MenuModule } from '../menu/menu.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: MenusComponent },
      { path: ':id', component: MenuViewComponent }
    ]),
    MenuModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class MenuRoutedModule {

}
