import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { Sort } from '@angular/material/sort';
import { forkJoin, of } from 'rxjs';
import { catchError, mergeMap } from 'rxjs/operators';
import { TranslocoService } from '@ngneat/transloco';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page as PageModel, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';
import { OrderDirectionManager } from '../core/models/order-direction.model';
import { ColumnDescription } from '../core/column-settings.component';
import { StoredValueService } from '../core/stored-value.service';

import { Page, PageListState } from '../page-core/page.model';
import { PageService } from '../page-core/page.service';
import { PageEditComponent } from './page-edit.component';

@Component({
  selector: 'app-page-list',
  templateUrl: './page-list.component.html',
  styleUrls: ['./page-list.component.scss']
})
export class PageListComponent implements OnInit {
  content: PageModel<Page>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  defaultColumns = [
    'select',
    'type',
    'name',
    'route',
    'modificationDate',
    'action'
  ];
  columns = this.storedValueService.getStoredValue('pageColumns', this.defaultColumns);
  columnDescriptions: ColumnDescription[] = [
    { name: 'select', isSystem: true },
    { name: 'type', displayName: this.translocoService.translate('Type') },
    { name: 'name', displayName: this.translocoService.translate('Name') },
    { name: 'shortName', displayName: this.translocoService.translate('Short Name') },
    { name: 'parent', displayName: this.translocoService.translate('Parent') },
    { name: 'header', displayName: this.translocoService.translate('Header') },
    { name: 'body', displayName: this.translocoService.translate('Body') },
    { name: 'metaTitle', displayName: this.translocoService.translate('Meta Title') },
    { name: 'metaKeywords', displayName: this.translocoService.translate('Meta Keywords') },
    { name: 'metaDescription', displayName: this.translocoService.translate('Meta Description') },
    { name: 'image', displayName: this.translocoService.translate('Image') },
    { name: 'preview', displayName: this.translocoService.translate('Preview') },
    { name: 'publicationDate', displayName: this.translocoService.translate('Publication Date') },
    { name: 'action', isSystem: true },
  ];
  selectedIds = new Set<number>();

  @Input()
  state: PageListState;

  @Input()
  baseRoute = '/page';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private pageService: PageService,
    private noticeHelper: NoticeHelper,
    private translocoService: TranslocoService,
    private storedValueService: StoredValueService
    ) {
  }

  ngOnInit() {
    this.getPages();
  }

  private getPages() {
    this.selectedIds = new Set<number>();
    this.pageService.getPages({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getPages();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.state.filter.type = null;
    this.getPages();
  }

  onCreate() {
    PageEditComponent.show(this.dialog, null).subscribe(() => {
      this.getPages();
    });
  }

  onExport(): void {
    this.pageService.exportPages({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    PageEditComponent.show(this.dialog, id).subscribe(() => {
      this.getPages();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.pageService.deletePage({ id }))
      )
      .subscribe(() => this.getPages(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.pageService.deletePage({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getPages());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getPages();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getPages();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
