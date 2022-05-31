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

import { Widget, WidgetListState } from '../widget-core/widget.model';
import { WidgetService } from '../widget-core/widget.service';
import { WidgetEditComponent } from './widget-edit.component';

@Component({
  selector: 'app-widget-list',
  templateUrl: './widget-list.component.html',
  styleUrls: ['./widget-list.component.scss']
})
export class WidgetListComponent implements OnInit {
  content: Page<Widget>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'select',
    'name',
    'code',
    'type',
    'action'
  ];
  selectedIds = new Set<number>();

  @Input()
  state: WidgetListState;

  @Input()
  baseRoute = '/widget';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private widgetService: WidgetService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getWidgets();
  }

  private getWidgets() {
    this.selectedIds = new Set<number>();
    this.widgetService.getWidgets({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getWidgets();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getWidgets();
  }

  onCreate() {
    WidgetEditComponent.show(this.dialog, null).subscribe(() => {
      this.getWidgets();
    });
  }

  onExport(): void {
    this.widgetService.exportWidgets({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    WidgetEditComponent.show(this.dialog, id).subscribe(() => {
      this.getWidgets();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.widgetService.deleteWidget({ id }))
      )
      .subscribe(() => this.getWidgets(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.widgetService.deleteWidget({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getWidgets());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getWidgets();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getWidgets();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
