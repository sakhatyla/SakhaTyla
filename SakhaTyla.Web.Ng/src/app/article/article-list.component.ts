import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';

import { Article, ArticleListState } from '../article-core/article.model';
import { ArticleService } from '../article-core/article.service';
import { ArticleEditComponent } from './article-edit.component';
import { ArticleChangesComponent } from './article-changes.component';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.scss']
})
export class ArticleListComponent implements OnInit {
  content: Page<Article>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'title',
    'text',
    'fromLanguage',
    'toLanguage',
    'fuzzy',
    'category',
    'action'
  ];

  @Input()
  state: ArticleListState;

  @Input()
  baseRoute = '/article';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private articleService: ArticleService,
    private noticeHelper: NoticeHelper
  ) {
  }

  ngOnInit() {
    this.getArticles();
  }

  private getArticles() {
    this.articleService.getArticles({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getArticles();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getArticles();
  }

  onCreate() {
    ArticleEditComponent.show(this.dialog, null).subscribe(() => {
      this.getArticles();
    });
  }

  onExport(): void {
    this.articleService.exportArticles({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    ArticleEditComponent.show(this.dialog, id).subscribe(() => {
      this.getArticles();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.articleService.deleteArticle({ id }))
      )
      .subscribe(() => this.getArticles(),
        error => this.onError(error));
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getArticles();
  }

  onViewChanges() {
    ArticleChangesComponent.show(this.dialog, null)
      .subscribe(() => { });
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
