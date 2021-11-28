import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';

import { FileGroup, FileGroupListState } from '../file-group-core/file-group.model';
import { FileGroupService } from '../file-group-core/file-group.service';
import { FileGroupEditComponent } from './file-group-edit.component';

@Component({
  selector: 'app-file-group-list',
  templateUrl: './file-group-list.component.html',
  styleUrls: ['./file-group-list.component.scss']
})
export class FileGroupListComponent implements OnInit {
  content: Page<FileGroup>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'name',
    'type',
    'location',
    'accept',
    'action'
  ];

  @Input()
  state: FileGroupListState;

  @Input()
  baseRoute = '/file-group';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private fileGroupService: FileGroupService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getFileGroups();
  }

  private getFileGroups() {
    this.fileGroupService.getFileGroups({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getFileGroups();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getFileGroups();
  }

  onCreate() {
    FileGroupEditComponent.show(this.dialog, null).subscribe(() => {
      this.getFileGroups();
    });
  }

  onExport(): void {
    this.fileGroupService.exportFileGroups({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    FileGroupEditComponent.show(this.dialog, id).subscribe(() => {
      this.getFileGroups();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.fileGroupService.deleteFileGroup({ id }))
      )
      .subscribe(() => this.getFileGroups(),
        error => this.onError(error));
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getFileGroups();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
