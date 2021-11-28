import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { UsersComponent } from './users.component';
import { UserViewComponent } from './user-view.component';
import { UserModule } from './user.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: UsersComponent },
      { path: ':id', component: UserViewComponent }
    ]),
    UserModule,
  ],
  exports: [
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class UserRoutedModule {

}
