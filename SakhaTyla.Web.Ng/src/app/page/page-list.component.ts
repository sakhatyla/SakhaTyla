import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { Sort } from '@angular/material/sort';
import { forkJoin, of } from 'rxjs';
import { catchError, mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page as PageModel, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';
import { OrderDirectionManager } from '../core/models/order-direction.model';

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
  columns = [
    'select',
    'type',
    'name',
    'route',
    'modificationDate',
    'action'
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
    private noticeHelper: NoticeHelper
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
