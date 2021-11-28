import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { FileGroup } from '../file-group-core/file-group.model';
import { FileGroupService } from '../file-group-core/file-group.service';

class DialogData {
  id: number;
}

@Component({
  selector: 'app-file-group-edit',
  templateUrl: './file-group-edit.component.html',
  styleUrls: ['./file-group-edit.component.scss']
})
export class FileGroupEditComponent implements OnInit {
  id: number;
  fileGroupForm = this.fb.group({
    id: [],
    name: [],
    type: [],
    location: [],
    accept: []
  });
  fileGroup: FileGroup;
  error: Error;

  constructor(public dialogRef: MatDialogRef<FileGroupEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private fileGroupService: FileGroupService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
  }

  static show(dialog: MatDialog, id: number): Observable<any> {
    const dialogRef = dialog.open(FileGroupEditComponent, {
      width: '600px',
      data: { id: id }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getFileGroup();
  }

  private getFileGroup() {
    const getFileGroup$ = !this.id ?
      of(new FileGroup()) :
      this.fileGroupService.getFileGroup({ id: this.id });
    getFileGroup$.subscribe(fileGroup => {
      this.fileGroup = fileGroup;
      this.fileGroupForm.patchValue(this.fileGroup);
    });
  }

  onSave(): void {
    this.saveFileGroup();
  }

  private saveFileGroup() {
    const saveFileGroup$ = this.id ?
      this.fileGroupService.updateFileGroup(this.fileGroupForm.value) :
      this.fileGroupService.createFileGroup(this.fileGroupForm.value);
    saveFileGroup$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.fileGroupForm, error);
    }
  }
}
