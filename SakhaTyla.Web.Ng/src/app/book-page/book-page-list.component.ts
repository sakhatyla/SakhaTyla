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

import { BookPage, BookPageListState } from '../book-page-core/book-page.model';
import { BookPageService } from '../book-page-core/book-page.service';
import { BookPageEditComponent } from './book-page-edit.component';

@Component({
  selector: 'app-book-page-list',
  templateUrl: './book-page-list.component.html',
  styleUrls: ['./book-page-list.component.scss']
})
export class BookPageListComponent implements OnInit {
  content: Page<BookPage>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'select',
    'fileName',
    'number',
    'action'
  ];
  selectedIds = new Set<number>();

  @Input()
  state: BookPageListState;

  @Input()
  baseRoute = '/book-page';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private bookPageService: BookPageService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getBookPages();
  }

  private getBookPages() {
    this.selectedIds = new Set<number>();
    this.bookPageService.getBookPages({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getBookPages();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getBookPages();
  }

  onCreate() {
    BookPageEditComponent.show(this.dialog, null, this.state.filter.bookId).subscribe(() => {
      this.getBookPages();
    });
  }

  onExport(): void {
    this.bookPageService.exportBookPages({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    BookPageEditComponent.show(this.dialog, id).subscribe(() => {
      this.getBookPages();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.bookPageService.deleteBookPage({ id }))
      )
      .subscribe(() => this.getBookPages(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.bookPageService.deleteBookPage({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getBookPages());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getBookPages();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getBookPages();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
