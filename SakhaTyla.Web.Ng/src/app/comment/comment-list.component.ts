import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { Sort } from '@angular/material/sort';
import { forkJoin, of } from 'rxjs';
import { catchError, mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';
import { OrderDirectionManager } from '../core/models/order-direction.model';

import { Comment, CommentListState } from '../comment-core/comment.model';
import { CommentService } from '../comment-core/comment.service';
import { CommentEditComponent } from './comment-edit.component';

@Component({
  selector: 'app-comment-list',
  templateUrl: './comment-list.component.html',
  styleUrls: ['./comment-list.component.scss']
})
export class CommentListComponent implements OnInit {
  comments: Comment[];

  @Input()
  state: CommentListState;

  @Input()
  baseRoute = '/comment';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private commentService: CommentService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getComments();
  }

  private getComments() {
    this.commentService.getComments({
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection,
      skipChildren: true
    }).subscribe(content => this.comments = this.getTree(content.pageItems));
  }

  private getTree(comments: Comment[]): Comment[] {
    const commentDic = {};
    for (const comment of comments) {
      commentDic[comment.id] = comment;
    }
    const rootComments: Comment[] = [];
    for (const comment of comments) {
      if (comment.parentId) {
        const parent = commentDic[comment.parentId];
        if (!parent.children) {
          parent.children = [];
        }
        parent.children.push(comment);
      } else {
        rootComments.push(comment);
      }
    }
    return rootComments;
  }

  onCreate(parentId?: number) {
    CommentEditComponent.show(this.dialog, null, this.state.filter.containerId, parentId).subscribe(() => {
      this.getComments();
    });
  }

  onEdit(id: number) {
    CommentEditComponent.show(this.dialog, id, this.state.filter.containerId, null).subscribe(() => {
      this.getComments();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.commentService.deleteComment({ id }))
      )
      .subscribe(() => this.getComments(),
        error => this.onError(error));
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
