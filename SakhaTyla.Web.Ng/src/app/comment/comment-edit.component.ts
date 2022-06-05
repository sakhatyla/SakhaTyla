import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { Comment } from '../comment-core/comment.model';
import { CommentService } from '../comment-core/comment.service';

class DialogData {
  id: number;
  containerId: number;
  parentId: number;
}

@Component({
  selector: 'app-comment-edit',
  templateUrl: './comment-edit.component.html',
  styleUrls: ['./comment-edit.component.scss']
})
export class CommentEditComponent implements OnInit {
  id: number;
  containerId: number;
  parentId: number;
  commentForm = this.fb.group({
    id: [],
    containerId: [],
    text: [],
    textSource: [],
    authorId: [],
    parentId: []
  });
  comment: Comment;
  error: Error;

  constructor(public dialogRef: MatDialogRef<CommentEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private commentService: CommentService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
    this.containerId = data.containerId;
    this.parentId = data.parentId;
  }

  static show(dialog: MatDialog, id: number, containerId: number, parentId: number): Observable<any> {
    const dialogRef = dialog.open(CommentEditComponent, {
      width: '600px',
      data: { id: id, containerId: containerId, parentId: parentId }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getComment();
  }

  private getComment() {
    const getComment$ = !this.id ?
      of(new Comment()) :
      this.commentService.getComment({ id: this.id });
    getComment$.subscribe(comment => {
      this.comment = comment;
      this.commentForm.patchValue(this.comment);
    });
  }

  onSave(): void {
    this.saveComment();
  }

  private saveComment() {
    const comment = this.commentForm.value;
    comment.containerId = this.containerId;
    comment.parentId = this.parentId;
    const saveComment$ = this.id ?
      this.commentService.updateComment(comment) :
      this.commentService.createComment(comment);
    saveComment$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.commentForm, error);
    }
  }
}
