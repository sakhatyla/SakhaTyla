import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { Page } from '../page-core/page.model';
import { PageService } from '../page-core/page.service';
import { PageEditComponent } from './page-edit.component';
import { CommentListState } from '../comment-core/comment.model';
import { PageType } from '../page-type/page-type.model';

@Component({
  selector: 'app-page-view',
  templateUrl: './page-view.component.html',
  styleUrls: ['./page-view.component.scss']
})
export class PageViewComponent implements OnInit {
  id: number;
  page: Page;
  commentListState = new CommentListState();

  PageType = PageType;

  constructor(private dialog: MatDialog,
              private pageService: PageService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getPage();
    });
  }

  private getPage() {
    this.pageService.getPage({ id: this.id })
      .subscribe(page => {
        this.page = page;
        this.commentListState.filter.containerId = this.page.commentContainerId;
      });
  }

  onEdit() {
    PageEditComponent.show(this.dialog, this.id).subscribe(() => {
      this.getPage();
    });
  }

  onBack(): void {
    window.history.back();
  }
}
