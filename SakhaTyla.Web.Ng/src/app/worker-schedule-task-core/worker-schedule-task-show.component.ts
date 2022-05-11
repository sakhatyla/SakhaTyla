import { Component, Input } from '@angular/core';

import { WorkerScheduleTask } from './worker-schedule-task.model';

@Component({
  selector: 'app-worker-schedule-task-show',
  templateUrl: './worker-schedule-task-show.component.html'
})

export class WorkerScheduleTaskShowComponent {
  @Input()
  value: WorkerScheduleTask;
}
