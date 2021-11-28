import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ProfileCommonModule } from './profile-common.module';

import { ProfileEditComponent } from './profile-edit.component';


@NgModule({
  declarations: [
  ],
  imports: [
    ProfileCommonModule,
    RouterModule.forChild([
      { path: '', component: ProfileEditComponent },
    ]),
  ],
  providers: [
  ],
  exports: [
  ]
})
export class ProfileModule {

}
