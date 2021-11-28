import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';

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
  columns = [
    'workerInfo',
    'status',
    'startDateTime',
    'endDateTime',
    'action'
  ];

  @Input()
  state: WorkerRunListState;

  @Input()
  baseRoute = '/worker-run';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private workerRunService: WorkerRunService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getWorkerRuns();
  }

  private getWorkerRuns() {
    this.workerRunService.getWorkerRuns({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getWorkerRuns();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
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

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getWorkerRuns();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
