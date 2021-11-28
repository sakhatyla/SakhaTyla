import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { WorkerRun } from '../worker-run-core/worker-run.model';
import { WorkerRunService } from '../worker-run-core/worker-run.service';

class DialogData {
  id: number;
}

@Component({
  selector: 'app-worker-run-edit',
  templateUrl: './worker-run-edit.component.html',
  styleUrls: ['./worker-run-edit.component.scss']
})
export class WorkerRunEditComponent implements OnInit {
  id: number;
  workerRunForm = this.fb.group({
    id: [],
    workerInfoId: [],
    data: [],
  });
  workerRun: WorkerRun;
  error: Error;

  constructor(public dialogRef: MatDialogRef<WorkerRunEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private workerRunService: WorkerRunService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
  }

  static show(dialog: MatDialog, id: number): Observable<any> {
    const dialogRef = dialog.open(WorkerRunEditComponent, {
      width: '600px',
      data: { id: id }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getWorkerRun();
  }

  private getWorkerRun() {
    const getWorkerRun$ = !this.id ?
      of(new WorkerRun()) :
      this.workerRunService.getWorkerRun({ id: this.id });
    getWorkerRun$.subscribe(workerRun => {
      this.workerRun = workerRun;
      this.workerRunForm.patchValue(this.workerRun);
    });
  }

  onSave(): void {
    this.saveWorkerRun();
  }

  private saveWorkerRun() {
    const saveWorkerRun$ =
      this.workerRunService.createWorkerRun(this.workerRunForm.value);
    saveWorkerRun$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.workerRunForm, error);
    }
  }
}
