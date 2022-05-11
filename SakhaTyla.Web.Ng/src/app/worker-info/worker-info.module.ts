import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { WorkerInfoCoreModule } from '../worker-info-core/worker-info-core.module';
import { WorkerScheduleTaskModule } from '../worker-schedule-task/worker-schedule-task.module';

import { WorkerInfoListComponent } from './worker-info-list.component';
import { WorkerInfoEditComponent } from './worker-info-edit.component';
import { WorkerInfoViewComponent } from './worker-info-view.component';
import { WorkerInfosComponent } from './worker-infos.component';

@NgModule({
  declarations: [
    WorkerInfoListComponent,
    WorkerInfoEditComponent,
    WorkerInfoViewComponent,
    WorkerInfosComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    WorkerInfoCoreModule,
    WorkerScheduleTaskModule
  ],
  exports: [
    WorkerInfoListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    WorkerInfoEditComponent
  ]
})
export class WorkerInfoModule {

}
