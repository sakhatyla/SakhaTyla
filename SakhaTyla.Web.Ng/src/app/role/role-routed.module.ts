import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { RolesComponent } from './roles.component';
import { RoleViewComponent } from './role-view.component';
import { RoleModule } from './role.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: RolesComponent },
      { path: ':id', component: RoleViewComponent }
    ]),
    RoleModule,
  ],
  exports: [
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class RoleRoutedModule {

}
