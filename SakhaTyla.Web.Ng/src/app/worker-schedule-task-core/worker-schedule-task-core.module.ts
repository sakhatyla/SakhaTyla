import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { WorkerScheduleTaskService } from './worker-schedule-task.service';
import { WorkerScheduleTaskSelectComponent } from './worker-schedule-task-select.component';
import { WorkerScheduleTaskShowComponent } from './worker-schedule-task-show.component';

@NgModule({
  declarations: [
    WorkerScheduleTaskSelectComponent,
    WorkerScheduleTaskShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    WorkerScheduleTaskService
  ],
  exports: [
    WorkerScheduleTaskSelectComponent,
    WorkerScheduleTaskShowComponent
  ]
})
export class WorkerScheduleTaskCoreModule {

}
