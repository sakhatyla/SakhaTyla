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

import { Book, BookListState } from '../book-core/book.model';
import { BookService } from '../book-core/book.service';
import { BookEditComponent } from './book-edit.component';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.scss']
})
export class BookListComponent implements OnInit {
  content: Page<Book>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'select',
    'name',
    'synonym',
    'hidden',
    'cover',
    'action'
  ];
  selectedIds = new Set<number>();

  @Input()
  state: BookListState;

  @Input()
  baseRoute = '/book';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private bookService: BookService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getBooks();
  }

  private getBooks() {
    this.selectedIds = new Set<number>();
    this.bookService.getBooks({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getBooks();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getBooks();
  }

  onCreate() {
    BookEditComponent.show(this.dialog, null).subscribe(() => {
      this.getBooks();
    });
  }

  onExport(): void {
    this.bookService.exportBooks({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    BookEditComponent.show(this.dialog, id).subscribe(() => {
      this.getBooks();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.bookService.deleteBook({ id }))
      )
      .subscribe(() => this.getBooks(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.bookService.deleteBook({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getBooks());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getBooks();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getBooks();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
