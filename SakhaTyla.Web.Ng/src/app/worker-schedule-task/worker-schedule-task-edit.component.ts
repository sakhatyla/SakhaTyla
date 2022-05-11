import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { WorkerScheduleTask } from '../worker-schedule-task-core/worker-schedule-task.model';
import { WorkerScheduleTaskService } from '../worker-schedule-task-core/worker-schedule-task.service';

class DialogData {
  id: number;
  workerInfoId?: number;
}

@Component({
  selector: 'app-worker-schedule-task-edit',
  templateUrl: './worker-schedule-task-edit.component.html',
  styleUrls: ['./worker-schedule-task-edit.component.scss']
})
export class WorkerScheduleTaskEditComponent implements OnInit {
  id: number;
  workerInfoId: number;
  workerScheduleTaskForm = this.fb.group({
    id: [],
    workerInfoId: [],
    seconds: [],
    minutes: [],
    hours: [],
    dayOfMonth: [],
    month: [],
    dayOfWeek: [],
    year: []
  });
  workerScheduleTask: WorkerScheduleTask;
  error: Error;

  constructor(public dialogRef: MatDialogRef<WorkerScheduleTaskEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private workerScheduleTaskService: WorkerScheduleTaskService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
    this.workerInfoId = data.workerInfoId;
  }

  static show(dialog: MatDialog, id: number, workerInfoId?: number): Observable<any> {
    const dialogRef = dialog.open(WorkerScheduleTaskEditComponent, {
      width: '600px',
      data: { id, workerInfoId }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getWorkerScheduleTask();
  }

  private getWorkerScheduleTask() {
    const getWorkerScheduleTask$ = !this.id ?
      of(new WorkerScheduleTask(this.workerInfoId)) :
      this.workerScheduleTaskService.getWorkerScheduleTask({ id: this.id });
    getWorkerScheduleTask$.subscribe(workerScheduleTask => {
      this.workerScheduleTask = workerScheduleTask;
      this.workerScheduleTaskForm.patchValue(this.workerScheduleTask);
    });
  }

  onSave(): void {
    this.saveWorkerScheduleTask();
  }

  private saveWorkerScheduleTask() {
    const saveWorkerScheduleTask$ = this.id ?
      this.workerScheduleTaskService.updateWorkerScheduleTask(this.workerScheduleTaskForm.value) :
      this.workerScheduleTaskService.createWorkerScheduleTask(this.workerScheduleTaskForm.value);
    saveWorkerScheduleTask$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.workerScheduleTaskForm, error);
    }
  }
}
