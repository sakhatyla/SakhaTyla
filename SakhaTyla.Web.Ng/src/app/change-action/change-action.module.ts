import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { ChangeActionViewComponent } from './change-action-view.component';
import { TranslocoRootModule } from '../transloco-root.module';

@NgModule({
  declarations: [
    ChangeActionViewComponent
  ],
  imports: [
    CoreModule,
    TranslocoRootModule,
  ],
  providers: [
  ],
  exports: [
    ChangeActionViewComponent
  ]
})
export class ChangeActionModule {

}
