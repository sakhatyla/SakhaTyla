import { Component, Input, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of, forkJoin } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { File } from '../file-core/file.model';
import { FileService } from '../file-core/file.service';
import { FileGroupService } from '../file-group-core/file-group.service';
import { FileGroup } from '../file-group-core/file-group.model';

class DialogData {
  id: number;
}

@Component({
  selector: 'app-file-edit',
  templateUrl: './file-edit.component.html',
  styleUrls: ['./file-edit.component.scss']
})
export class FileEditComponent implements OnInit {
  id: number;
  fileForm = this.fb.group({
    id: [],
    name: [],
    contentType: [],
    content: [],
    url: [],
    groupId: []
  });
  file: File;
  error: Error;
  group: FileGroup;

  @ViewChild('fileInput')
  fileInputEl: ElementRef<HTMLInputElement>;

  constructor(public dialogRef: MatDialogRef<FileEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private fileService: FileService,
              private fileGroupService: FileGroupService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
    this.fileForm.controls.groupId.valueChanges
      .subscribe(groupId => {
        if (groupId) {
          this.fileGroupService.getFileGroup({ id: groupId })
            .subscribe(group => this.group = group);
        } else {
          this.group = null;
        }
      });
  }

  static show(dialog: MatDialog, id: number): Observable<any> {
    const dialogRef = dialog.open(FileEditComponent, {
      width: '600px',
      data: { id: id }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getFile();
  }

  private getFile() {
    const getFile$ = !this.id ?
      of(new File()) :
      this.fileService.getFile({ id: this.id });
    getFile$.subscribe(file => {
      this.file = file;
      this.fileForm.patchValue(this.file);
    });
  }

  onSave(): void {
    this.saveFile();
  }

  private saveFile() {
    const input = this.fileInputEl.nativeElement;
    const saveFile$ = this.id ?
      input.files[0] ? this.fileService.updateFile({ id: this.id, file: input.files[0] }) : of() :
      forkJoin(Array.from(input.files).map(file => this.fileService.createFile({ groupId: this.group.id, file: file })));
    saveFile$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.fileForm, error);
    }
  }
}
