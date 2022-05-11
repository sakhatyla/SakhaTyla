import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { WorkerScheduleTasksComponent } from './worker-schedule-tasks.component';
import { WorkerScheduleTaskViewComponent } from './worker-schedule-task-view.component';
import { WorkerScheduleTaskModule } from '../worker-schedule-task/worker-schedule-task.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: WorkerScheduleTasksComponent },
      { path: ':id', component: WorkerScheduleTaskViewComponent }
    ]),
    WorkerScheduleTaskModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class WorkerScheduleTaskRoutedModule {

}
