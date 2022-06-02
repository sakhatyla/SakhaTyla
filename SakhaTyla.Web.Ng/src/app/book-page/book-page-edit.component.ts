import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { BookPage } from '../book-page-core/book-page.model';
import { BookPageService } from '../book-page-core/book-page.service';

class DialogData {
  id: number;
  bookId?: number;
}

@Component({
  selector: 'app-book-page-edit',
  templateUrl: './book-page-edit.component.html',
  styleUrls: ['./book-page-edit.component.scss']
})
export class BookPageEditComponent implements OnInit {
  id: number;
  bookId?: number;
  bookPageForm = this.fb.group({
    id: [],
    bookId: [],
    fileName: [],
    number: []
  });
  bookPage: BookPage;
  error: Error;

  constructor(public dialogRef: MatDialogRef<BookPageEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private bookPageService: BookPageService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
    this.bookId = data.bookId;
  }

  static show(dialog: MatDialog, id: number, bookId?: number): Observable<any> {
    const dialogRef = dialog.open(BookPageEditComponent, {
      width: '600px',
      data: { id: id, bookId: bookId }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getBookPage();
  }

  private getBookPage() {
    const getBookPage$ = !this.id ?
      of(new BookPage()) :
      this.bookPageService.getBookPage({ id: this.id });
    getBookPage$.subscribe(bookPage => {
      this.bookPage = bookPage;
      this.bookPageForm.patchValue(this.bookPage);
    });
  }

  onSave(): void {
    this.saveBookPage();
  }

  private saveBookPage() {
    const book = this.bookPageForm.value;
    book.bookId = this.bookId;
    const saveBookPage$ = this.id ?
      this.bookPageService.updateBookPage(book) :
      this.bookPageService.createBookPage(book);
    saveBookPage$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.bookPageForm, error);
    }
  }
}
