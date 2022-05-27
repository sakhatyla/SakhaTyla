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

import { Tag, TagListState } from '../tag-core/tag.model';
import { TagService } from '../tag-core/tag.service';
import { TagEditComponent } from './tag-edit.component';

@Component({
  selector: 'app-tag-list',
  templateUrl: './tag-list.component.html',
  styleUrls: ['./tag-list.component.scss']
})
export class TagListComponent implements OnInit {
  content: Page<Tag>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'select',
    'name',
    'action'
  ];
  selectedIds = new Set<number>();

  @Input()
  state: TagListState;

  @Input()
  baseRoute = '/tag';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private tagService: TagService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getTags();
  }

  private getTags() {
    this.selectedIds = new Set<number>();
    this.tagService.getTags({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getTags();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getTags();
  }

  onCreate() {
    TagEditComponent.show(this.dialog, null).subscribe(() => {
      this.getTags();
    });
  }

  onExport(): void {
    this.tagService.exportTags({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    TagEditComponent.show(this.dialog, id).subscribe(() => {
      this.getTags();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.tagService.deleteTag({ id }))
      )
      .subscribe(() => this.getTags(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.tagService.deleteTag({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getTags());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getTags();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getTags();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
