import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';

import { ProfileService } from './profile.service';
import { ProfileEditComponent } from './profile-edit.component';

@NgModule({
  declarations: [
    ProfileEditComponent,
  ],
  imports: [
    CoreModule,
    TranslocoRootModule,
    RouterModule,
  ],
  providers: [
    ProfileService
  ],
  exports: [
    ProfileEditComponent
  ]
})
export class ProfileCommonModule {

}
