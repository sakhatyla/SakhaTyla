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

import { File, FileListState } from '../file-core/file.model';
import { FileService } from '../file-core/file.service';
import { FileEditComponent } from './file-edit.component';

@Component({
  selector: 'app-file-list',
  templateUrl: './file-list.component.html',
  styleUrls: ['./file-list.component.scss']
})
export class FileListComponent implements OnInit {
  content: Page<File>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'select',
    'name',
    'contentType',
    'url',
    'group',
    'action'
  ];
  selectedIds = new Set<number>();

  @Input()
  state: FileListState;

  @Input()
  baseRoute = '/file';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private fileService: FileService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getFiles();
  }

  private getFiles() {
    this.selectedIds = new Set<number>();
    this.fileService.getFiles({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getFiles();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.state.filter.groupId = null;
    this.getFiles();
  }

  onCreate() {
    FileEditComponent.show(this.dialog, null).subscribe(() => {
      this.getFiles();
    });
  }

  onExport(): void {
    this.fileService.exportFiles({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    FileEditComponent.show(this.dialog, id).subscribe(() => {
      this.getFiles();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.fileService.deleteFile({ id }))
      )
      .subscribe(() => this.getFiles(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.fileService.deleteFile({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getFiles());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getFiles();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getFiles();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
