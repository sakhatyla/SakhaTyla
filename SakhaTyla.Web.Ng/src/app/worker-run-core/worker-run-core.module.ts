import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { WorkerRunService } from './worker-run.service';
import { WorkerRunSelectComponent } from './worker-run-select.component';
import { WorkerRunShowComponent } from './worker-run-show.component';

@NgModule({
  declarations: [
    WorkerRunSelectComponent,
    WorkerRunShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    WorkerRunService
  ],
  exports: [
    WorkerRunSelectComponent,
    WorkerRunShowComponent
  ]
})
export class WorkerRunCoreModule {

}
