import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { WorkerScheduleTaskListState } from '../worker-schedule-task-core/worker-schedule-task.model';

@Component({
  selector: 'app-worker-schedule-tasks',
  templateUrl: './worker-schedule-tasks.component.html',
  styleUrls: ['./worker-schedule-tasks.component.scss']
})
export class WorkerScheduleTasksComponent {

  state: WorkerScheduleTaskListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('workerScheduleTaskListState', new WorkerScheduleTaskListState());
  }

}
