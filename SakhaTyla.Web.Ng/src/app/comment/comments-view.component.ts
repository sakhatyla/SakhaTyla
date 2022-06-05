import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { CommentContainer, CommentListState } from '../comment-core/comment.model';
import { CommentService } from '../comment-core/comment.service';
import { ConfigService } from '../config/config.service';

@Component({
  selector: 'app-comments-view',
  templateUrl: './comments-view.component.html',
  styleUrls: ['./comments-view.component.scss']
})
export class CommentsViewComponent implements OnInit {
  commentContainer: CommentContainer;
  containerId: number;
  commentListState: CommentListState;

  siteUrl = ''; // TODO: this.configService.config.siteUrl;
  pageUrl: string;

  constructor(
    private configService: ConfigService,
    private commentService: CommentService,
    private dialog: MatDialog,
    private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.containerId = ConvertStringTo.number(params.containerId);
      this.getCommentContainer();
    });
  }

  private getCommentContainer(): void {
    this.commentService.getCommentContainer({ id: this.containerId })
      .subscribe(commentContainer => {
        this.commentContainer = commentContainer;
        this.commentListState = new CommentListState();
        this.commentListState.filter.containerId = this.containerId;
        this.pageUrl = this.siteUrl;
        if (this.commentContainer.page) {
          this.pageUrl += `/${this.commentContainer.page.route.path}`;
        }
      });
  }

  onBack(): void {
    window.history.back();
  }
}
