import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { Tag } from '../tag-core/tag.model';
import { TagService } from '../tag-core/tag.service';

class DialogData {
  id: number;
}

@Component({
  selector: 'app-tag-edit',
  templateUrl: './tag-edit.component.html',
  styleUrls: ['./tag-edit.component.scss']
})
export class TagEditComponent implements OnInit {
  id: number;
  tagForm = this.fb.group({
    id: [],
    name: []
  });
  tag: Tag;
  error: Error;

  constructor(public dialogRef: MatDialogRef<TagEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private tagService: TagService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
  }

  static show(dialog: MatDialog, id: number): Observable<any> {
    const dialogRef = dialog.open(TagEditComponent, {
      width: '600px',
      data: { id: id }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getTag();
  }

  private getTag() {
    const getTag$ = !this.id ?
      of(new Tag()) :
      this.tagService.getTag({ id: this.id });
    getTag$.subscribe(tag => {
      this.tag = tag;
      this.tagForm.patchValue(this.tag);
    });
  }

  onSave(): void {
    this.saveTag();
  }

  private saveTag() {
    const saveTag$ = this.id ?
      this.tagService.updateTag(this.tagForm.value) :
      this.tagService.createTag(this.tagForm.value);
    saveTag$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.tagForm, error);
    }
  }
}
