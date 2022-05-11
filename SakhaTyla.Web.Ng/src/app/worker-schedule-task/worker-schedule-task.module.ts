import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { WorkerScheduleTaskCoreModule } from '../worker-schedule-task-core/worker-schedule-task-core.module';
import { WorkerInfoCoreModule } from '../worker-info-core/worker-info-core.module';

import { WorkerScheduleTaskListComponent } from './worker-schedule-task-list.component';
import { WorkerScheduleTaskEditComponent } from './worker-schedule-task-edit.component';
import { WorkerScheduleTaskViewComponent } from './worker-schedule-task-view.component';
import { WorkerScheduleTasksComponent } from './worker-schedule-tasks.component';

@NgModule({
  declarations: [
    WorkerScheduleTaskListComponent,
    WorkerScheduleTaskEditComponent,
    WorkerScheduleTaskViewComponent,
    WorkerScheduleTasksComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    WorkerInfoCoreModule,
    WorkerScheduleTaskCoreModule
  ],
  exports: [
    WorkerScheduleTaskListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    WorkerScheduleTaskEditComponent
  ]
})
export class WorkerScheduleTaskModule {

}
