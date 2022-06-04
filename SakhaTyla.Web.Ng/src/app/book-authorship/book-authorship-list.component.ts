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

import { BookAuthorship, BookAuthorshipListState } from '../book-authorship-core/book-authorship.model';
import { BookAuthorshipService } from '../book-authorship-core/book-authorship.service';
import { BookAuthorshipEditComponent } from './book-authorship-edit.component';

@Component({
  selector: 'app-book-authorship-list',
  templateUrl: './book-authorship-list.component.html',
  styleUrls: ['./book-authorship-list.component.scss']
})
export class BookAuthorshipListComponent implements OnInit {
  content: Page<BookAuthorship>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'select',
    'author',
    'weight',
    'action'
  ];
  selectedIds = new Set<number>();

  @Input()
  state: BookAuthorshipListState;

  @Input()
  baseRoute = '/book-authorship';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private bookAuthorshipService: BookAuthorshipService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getBookAuthorships();
  }

  private getBookAuthorships() {
    this.selectedIds = new Set<number>();
    this.bookAuthorshipService.getBookAuthorships({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getBookAuthorships();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getBookAuthorships();
  }

  onCreate() {
    BookAuthorshipEditComponent.show(this.dialog, null, this.state.filter.bookId).subscribe(() => {
      this.getBookAuthorships();
    });
  }

  onExport(): void {
    this.bookAuthorshipService.exportBookAuthorships({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    BookAuthorshipEditComponent.show(this.dialog, id).subscribe(() => {
      this.getBookAuthorships();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.bookAuthorshipService.deleteBookAuthorship({ id }))
      )
      .subscribe(() => this.getBookAuthorships(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.bookAuthorshipService.deleteBookAuthorship({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getBookAuthorships());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getBookAuthorships();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getBookAuthorships();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
