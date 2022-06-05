import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';

import { Comment, CommentListState } from '../comment-core/comment.model';
import { CommentService } from '../comment-core/comment.service';
import { CommentEditComponent } from './comment-edit.component';
import { OrderDirection } from '../core/models/order-direction.model';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent implements OnInit {
  content: Page<Comment>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'creationDate',
    'page',
    'text',
    'action'
  ];

  state: CommentListState;
  baseRoute = '/comment';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private commentService: CommentService,
    private noticeHelper: NoticeHelper,
    private storeService: StoreService
  ) {
    const state = new CommentListState();
    this.state = this.storeService.get('commentListState', state);
  }

  ngOnInit() {
    this.getComments();
  }

  private getComments() {
    this.commentService.getComments({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: 'CreationDate',
      orderDirection: OrderDirection.Descending
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.getComments();
  }

  onReset() {
    this.state.filter.text = null;
    this.getComments();
    return false;
  }

  onExport(): void {
    this.commentService.exportComments({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
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

  onEdit(id: number) {
    CommentEditComponent.show(this.dialog, id, null, null).subscribe(() => {
      this.getComments();
    });
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getComments();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
