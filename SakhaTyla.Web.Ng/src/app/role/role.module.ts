import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { RoleCoreModule } from '../role-core/role-core.module';

import { RoleListComponent } from './role-list.component';
import { RoleEditComponent } from './role-edit.component';
import { RoleViewComponent } from './role-view.component';
import { RolesComponent } from './roles.component';

@NgModule({
  declarations: [
    RoleListComponent,
    RoleEditComponent,
    RoleViewComponent,
    RolesComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    RoleCoreModule
  ],
  exports: [
    RoleListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    RoleEditComponent
  ]
})
export class RoleModule {

}
