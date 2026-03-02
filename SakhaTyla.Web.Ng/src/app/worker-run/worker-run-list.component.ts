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
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';
import { OrderDirectionManager } from '../core/models/order-direction.model';
import { ColumnDescription } from '../core/column-settings.component';
import { StoredValueService } from '../core/stored-value.service';

import { WorkerRun, WorkerRunListState } from '../worker-run-core/worker-run.model';
import { WorkerRunService } from '../worker-run-core/worker-run.service';
import { WorkerRunEditComponent } from './worker-run-edit.component';

@Component({
  selector: 'app-worker-run-list',
  templateUrl: './worker-run-list.component.html',
  styleUrls: ['./worker-run-list.component.scss']
})
export class WorkerRunListComponent implements OnInit {
  content: Page<WorkerRun>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  defaultColumns = [
    'select',
    'workerInfo',
    'status',
    'startDateTime',
    'endDateTime',
    'action'
  ];
  columns = this.storedValueService.getStoredValue('workerRunColumns', this.defaultColumns);
  columnDescriptions: ColumnDescription[] = [
    { name: 'select', isSystem: true },
    { name: 'workerInfo', displayName: this.translocoService.translate('Worker') },
    { name: 'status', displayName: this.translocoService.translate('Status') },
    { name: 'startDateTime', displayName: this.translocoService.translate('Start Date') },
    { name: 'endDateTime', displayName: this.translocoService.translate('End Date') },
    { name: 'data', displayName: this.translocoService.translate('Data') },
    { name: 'result', displayName: this.translocoService.translate('Result') },
    { name: 'resultData', displayName: this.translocoService.translate('Result Data') },
    { name: 'action', isSystem: true },
  ];
  selectedIds = new Set<number>();

  @Input()
  state: WorkerRunListState;

  @Input()
  baseRoute = '/worker-run';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private workerRunService: WorkerRunService,
    private noticeHelper: NoticeHelper,
    private translocoService: TranslocoService,
    private storedValueService: StoredValueService
    ) {
  }

  ngOnInit() {
    this.getWorkerRuns();
  }

  private getWorkerRuns() {
    this.selectedIds = new Set<number>();
    this.workerRunService.getWorkerRuns({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getWorkerRuns();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.state.filter.workerInfoId = null;
    this.getWorkerRuns();
  }

  onCreate() {
    WorkerRunEditComponent.show(this.dialog, null).subscribe(() => {
      this.getWorkerRuns();
    });
  }

  onExport(): void {
    this.workerRunService.exportWorkerRuns({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.workerRunService.deleteWorkerRun({ id }))
      )
      .subscribe(() => this.getWorkerRuns(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.workerRunService.deleteWorkerRun({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getWorkerRuns());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getWorkerRuns();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getWorkerRuns();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
