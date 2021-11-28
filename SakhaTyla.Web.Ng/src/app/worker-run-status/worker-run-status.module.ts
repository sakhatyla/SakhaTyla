import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { WorkerRunStatusSelectComponent } from './worker-run-status-select.component';
import { WorkerRunStatusViewComponent } from './worker-run-status-view.component';

@NgModule({
  declarations: [
    WorkerRunStatusSelectComponent,
    WorkerRunStatusViewComponent
  ],
  imports: [
    CoreModule
  ],
  providers: [
  ],
  exports: [
    WorkerRunStatusSelectComponent,
    WorkerRunStatusViewComponent
  ]
})
export class WorkerRunStatusModule {

}
