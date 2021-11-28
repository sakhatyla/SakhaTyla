import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { WorkerInfoService } from './worker-info.service';
import { WorkerInfoSelectComponent } from './worker-info-select.component';
import { WorkerInfoShowComponent } from './worker-info-show.component';

@NgModule({
  declarations: [
    WorkerInfoSelectComponent,
    WorkerInfoShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    WorkerInfoService
  ],
  exports: [
    WorkerInfoSelectComponent,
    WorkerInfoShowComponent
  ]
})
export class WorkerInfoCoreModule {

}
