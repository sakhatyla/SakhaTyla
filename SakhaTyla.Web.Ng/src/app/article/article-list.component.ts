import { Component, OnInit, Input } from '@angular/core';
import { LegacyPageEvent as PageEvent } from '@angular/material/legacy-paginator';
import { MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { Sort } from '@angular/material/sort';
import { forkJoin, of } from 'rxjs';
import { catchError, mergeMap } from 'rxjs/operators';
import { TranslocoService } from '@ngneat/transloco';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';
import { OrderDirectionManager } from '../core/models/order-direction.model';
import { ColumnDescription } from '../core/column-settings.component';
import { StoredValueService } from '../core/stored-value.service';

import { Article, ArticleListState } from '../article-core/article.model';
import { ArticleService } from '../article-core/article.service';
import { ArticleEditComponent } from './article-edit.component';
import { ArticleImportComponent } from './article-import.component';
import { ArticleChangesComponent } from './article-changes.component';

@Component({
  selector: 'app-article-list',
  templateUrl: './article-list.component.html',
  styleUrls: ['./article-list.component.scss']
})
export class ArticleListComponent implements OnInit {
  content: Page<Article>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  defaultColumns = [
    'select',
    'title',
    'text',
    'fromLanguage',
    'toLanguage',
    'fuzzy',
    'category',
    'action'
  ];
  columnDescriptions: ColumnDescription[] = [
    { name: 'select', isSystem: true },
    { name: 'title', displayName: this.translocoService.translate('Title') },
    { name: 'text', displayName: this.translocoService.translate('Text') },
    { name: 'fromLanguage', displayName: this.translocoService.translate('From Language') },
    { name: 'toLanguage', displayName: this.translocoService.translate('To Language') },
    { name: 'fuzzy', displayName: this.translocoService.translate('Fuzzy') },
    { name: 'category', displayName: this.translocoService.translate('Category') },
    { name: 'action', isSystem: true },
  ];
  columns = this.storedValueService.getStoredValue('articleColumns', this.defaultColumns,
    ColumnDescription.filter(this.columnDescriptions));
  selectedIds = new Set<number>();

  @Input()
  state: ArticleListState;

  @Input()
  baseRoute = '/article';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private articleService: ArticleService,
    private noticeHelper: NoticeHelper,
    private translocoService: TranslocoService,
    private storedValueService: StoredValueService
  ) {
  }

  ngOnInit() {
    this.getArticles();
  }

  private getArticles() {
    this.selectedIds = new Set<number>();
    this.articleService.getArticles({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getArticles();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.title = null;
    this.state.filter.text = null;
    this.state.filter.fromLanguageId = null;
    this.state.filter.toLanguageId = null;
    this.state.filter.categoryId = null;
    this.state.filter.tagId = null;
    this.getArticles();
  }

  onCreate() {
    ArticleEditComponent.show(this.dialog, null).subscribe(() => {
      this.getArticles();
    });
  }

  onImport() {
    ArticleImportComponent.show(this.dialog).subscribe(() => {
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

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.articleService.deleteArticle({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getArticles());
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

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getArticles();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
