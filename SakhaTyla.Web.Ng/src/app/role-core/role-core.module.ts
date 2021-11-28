import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { RoleService } from './role.service';
import { RoleSelectComponent } from './role-select.component';
import { RoleShowComponent } from './role-show.component';
import { RolesSelectComponent } from './roles-select.component';
import { RolesShowComponent } from './roles-show.component';

@NgModule({
  declarations: [
    RoleSelectComponent,
    RoleShowComponent,
    RolesSelectComponent,
    RolesShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    RoleService
  ],
  exports: [
    RoleSelectComponent,
    RoleShowComponent,
    RolesSelectComponent,
    RolesShowComponent
  ]
})
export class RoleCoreModule {

}
