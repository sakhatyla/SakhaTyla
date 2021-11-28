import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { WorkerRunCoreModule } from '../worker-run-core/worker-run-core.module';
import { WorkerInfoCoreModule } from '../worker-info-core/worker-info-core.module';
import { WorkerRunStatusModule } from '../worker-run-status/worker-run-status.module';

import { WorkerRunListComponent } from './worker-run-list.component';
import { WorkerRunEditComponent } from './worker-run-edit.component';
import { WorkerRunViewComponent } from './worker-run-view.component';
import { WorkerRunsComponent } from './worker-runs.component';

@NgModule({
  declarations: [
    WorkerRunListComponent,
    WorkerRunEditComponent,
    WorkerRunViewComponent,
    WorkerRunsComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    WorkerInfoCoreModule,
    WorkerRunStatusModule,
    WorkerRunCoreModule
  ],
  exports: [
    WorkerRunListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    WorkerRunEditComponent
  ]
})
export class WorkerRunModule {

}
