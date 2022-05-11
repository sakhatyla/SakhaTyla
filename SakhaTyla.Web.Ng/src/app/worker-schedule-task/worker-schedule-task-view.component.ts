import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { WorkerScheduleTask } from '../worker-schedule-task-core/worker-schedule-task.model';
import { WorkerScheduleTaskService } from '../worker-schedule-task-core/worker-schedule-task.service';
import { WorkerScheduleTaskEditComponent } from './worker-schedule-task-edit.component';

@Component({
  selector: 'app-worker-schedule-task-view',
  templateUrl: './worker-schedule-task-view.component.html',
  styleUrls: ['./worker-schedule-task-view.component.scss']
})
export class WorkerScheduleTaskViewComponent implements OnInit {
  id: number;
  workerScheduleTask: WorkerScheduleTask;

  constructor(private dialog: MatDialog,
              private workerScheduleTaskService: WorkerScheduleTaskService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getWorkerScheduleTask();
    });
  }

  private getWorkerScheduleTask() {
    this.workerScheduleTaskService.getWorkerScheduleTask({ id: this.id })
      .subscribe(workerScheduleTask => this.workerScheduleTask = workerScheduleTask);
  }

  onEdit() {
    WorkerScheduleTaskEditComponent.show(this.dialog, this.id).subscribe(() => {
      this.getWorkerScheduleTask();
    });
  }

  onBack(): void {
    window.history.back();
  }
}
