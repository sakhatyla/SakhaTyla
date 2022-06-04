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

import { BookAuthor, BookAuthorListState } from '../book-author-core/book-author.model';
import { BookAuthorService } from '../book-author-core/book-author.service';
import { BookAuthorEditComponent } from './book-author-edit.component';

@Component({
  selector: 'app-book-author-list',
  templateUrl: './book-author-list.component.html',
  styleUrls: ['./book-author-list.component.scss']
})
export class BookAuthorListComponent implements OnInit {
  content: Page<BookAuthor>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'select',
    'lastName',
    'firstName',
    'middleName',
    'nickName',
    'action'
  ];
  selectedIds = new Set<number>();

  @Input()
  state: BookAuthorListState;

  @Input()
  baseRoute = '/book-author';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private bookAuthorService: BookAuthorService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getBookAuthors();
  }

  private getBookAuthors() {
    this.selectedIds = new Set<number>();
    this.bookAuthorService.getBookAuthors({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getBookAuthors();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getBookAuthors();
  }

  onCreate() {
    BookAuthorEditComponent.show(this.dialog, null).subscribe(() => {
      this.getBookAuthors();
    });
  }

  onExport(): void {
    this.bookAuthorService.exportBookAuthors({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    BookAuthorEditComponent.show(this.dialog, id).subscribe(() => {
      this.getBookAuthors();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.bookAuthorService.deleteBookAuthor({ id }))
      )
      .subscribe(() => this.getBookAuthors(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.bookAuthorService.deleteBookAuthor({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getBookAuthors());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getBookAuthors();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getBookAuthors();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
