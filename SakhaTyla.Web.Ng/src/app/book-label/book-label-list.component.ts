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

import { BookLabel, BookLabelListState } from '../book-label-core/book-label.model';
import { BookLabelService } from '../book-label-core/book-label.service';
import { BookLabelEditComponent } from './book-label-edit.component';

@Component({
  selector: 'app-book-label-list',
  templateUrl: './book-label-list.component.html',
  styleUrls: ['./book-label-list.component.scss']
})
export class BookLabelListComponent implements OnInit {
  content: Page<BookLabel>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'select',
    'name',
    'page',
    'action'
  ];
  selectedIds = new Set<number>();

  @Input()
  state: BookLabelListState;

  @Input()
  baseRoute = '/book-label';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private bookLabelService: BookLabelService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getBookLabels();
  }

  private getBookLabels() {
    this.selectedIds = new Set<number>();
    this.bookLabelService.getBookLabels({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getBookLabels();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getBookLabels();
  }

  onCreate() {
    BookLabelEditComponent.show(this.dialog, null, this.state.filter.bookId).subscribe(() => {
      this.getBookLabels();
    });
  }

  onExport(): void {
    this.bookLabelService.exportBookLabels({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    BookLabelEditComponent.show(this.dialog, id).subscribe(() => {
      this.getBookLabels();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.bookLabelService.deleteBookLabel({ id }))
      )
      .subscribe(() => this.getBookLabels(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.bookLabelService.deleteBookLabel({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getBookLabels());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getBookLabels();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getBookLabels();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
