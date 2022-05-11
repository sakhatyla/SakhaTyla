import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { WorkerInfo } from '../worker-info-core/worker-info.model';
import { WorkerInfoService } from '../worker-info-core/worker-info.service';
import { WorkerInfoEditComponent } from './worker-info-edit.component';
import { WorkerScheduleTaskListState } from '../worker-schedule-task-core/worker-schedule-task.model';
import { WorkerScheduleTaskFilter } from '../worker-schedule-task-core/worker-schedule-task-filter.model';

@Component({
  selector: 'app-worker-info-view',
  templateUrl: './worker-info-view.component.html',
  styleUrls: ['./worker-info-view.component.scss']
})
export class WorkerInfoViewComponent implements OnInit {
  id: number;
  workerInfo: WorkerInfo;
  workerScheduleTaskListState: WorkerScheduleTaskListState;

  constructor(private dialog: MatDialog,
              private workerInfoService: WorkerInfoService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getWorkerInfo();
    });
  }

  private getWorkerInfo() {
    this.workerInfoService.getWorkerInfo({ id: this.id })
      .subscribe(workerInfo => {
        this.workerInfo = workerInfo;
        this.workerScheduleTaskListState = new WorkerScheduleTaskListState(new WorkerScheduleTaskFilter(this.workerInfo.id));
      });
  }

  onEdit() {
    WorkerInfoEditComponent.show(this.dialog, this.id).subscribe(() => {
      this.getWorkerInfo();
    });
  }

  onBack(): void {
    window.history.back();
  }
}
