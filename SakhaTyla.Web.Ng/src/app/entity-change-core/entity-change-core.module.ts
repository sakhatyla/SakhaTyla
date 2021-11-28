import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { EntityChangeService } from './entity-change.service';

@NgModule({
  declarations: [
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    EntityChangeService
  ],
  exports: [
  ]
})
export class EntityChangeCoreModule {

}
