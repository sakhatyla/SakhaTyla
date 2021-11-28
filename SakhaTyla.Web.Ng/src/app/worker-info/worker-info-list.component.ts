import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';

import { WorkerInfo, WorkerInfoListState } from '../worker-info-core/worker-info.model';
import { WorkerInfoService } from '../worker-info-core/worker-info.service';
import { WorkerInfoEditComponent } from './worker-info-edit.component';

@Component({
  selector: 'app-worker-info-list',
  templateUrl: './worker-info-list.component.html',
  styleUrls: ['./worker-info-list.component.scss']
})
export class WorkerInfoListComponent implements OnInit {
  content: Page<WorkerInfo>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'name',
    'className',
    'action'
  ];

  @Input()
  state: WorkerInfoListState;

  @Input()
  baseRoute = '/worker-info';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private workerInfoService: WorkerInfoService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getWorkerInfos();
  }

  private getWorkerInfos() {
    this.workerInfoService.getWorkerInfos({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getWorkerInfos();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getWorkerInfos();
  }

  onCreate() {
    WorkerInfoEditComponent.show(this.dialog, null).subscribe(() => {
      this.getWorkerInfos();
    });
  }

  onExport(): void {
    this.workerInfoService.exportWorkerInfos({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    WorkerInfoEditComponent.show(this.dialog, id).subscribe(() => {
      this.getWorkerInfos();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.workerInfoService.deleteWorkerInfo({ id }))
      )
      .subscribe(() => this.getWorkerInfos(),
        error => this.onError(error));
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getWorkerInfos();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
