import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { WorkerInfo } from '../worker-info-core/worker-info.model';
import { WorkerInfoService } from '../worker-info-core/worker-info.service';

class DialogData {
  id: number;
}

@Component({
  selector: 'app-worker-info-edit',
  templateUrl: './worker-info-edit.component.html',
  styleUrls: ['./worker-info-edit.component.scss']
})
export class WorkerInfoEditComponent implements OnInit {
  id: number;
  workerInfoForm = this.fb.group({
    id: [],
    name: [],
    className: []
  });
  workerInfo: WorkerInfo;
  error: Error;

  constructor(public dialogRef: MatDialogRef<WorkerInfoEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private workerInfoService: WorkerInfoService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
  }

  static show(dialog: MatDialog, id: number): Observable<any> {
    const dialogRef = dialog.open(WorkerInfoEditComponent, {
      width: '600px',
      data: { id: id }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getWorkerInfo();
  }

  private getWorkerInfo() {
    const getWorkerInfo$ = !this.id ?
      of(new WorkerInfo()) :
      this.workerInfoService.getWorkerInfo({ id: this.id });
    getWorkerInfo$.subscribe(workerInfo => {
      this.workerInfo = workerInfo;
      this.workerInfoForm.patchValue(this.workerInfo);
    });
  }

  onSave(): void {
    this.saveWorkerInfo();
  }

  private saveWorkerInfo() {
    const saveWorkerInfo$ = this.id ?
      this.workerInfoService.updateWorkerInfo(this.workerInfoForm.value) :
      this.workerInfoService.createWorkerInfo(this.workerInfoForm.value);
    saveWorkerInfo$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.workerInfoForm, error);
    }
  }
}
