import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { MenuCoreModule } from '../menu-core/menu-core.module';

import { MenuListComponent } from './menu-list.component';
import { MenuEditComponent } from './menu-edit.component';
import { MenuViewComponent } from './menu-view.component';
import { MenusComponent } from './menus.component';

@NgModule({
  declarations: [
    MenuListComponent,
    MenuEditComponent,
    MenuViewComponent,
    MenusComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    MenuCoreModule
  ],
  exports: [
    MenuListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    MenuEditComponent
  ]
})
export class MenuModule {

}
