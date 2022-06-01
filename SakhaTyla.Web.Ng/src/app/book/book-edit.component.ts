import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { Book } from '../book-core/book.model';
import { BookService } from '../book-core/book.service';

class DialogData {
  id: number;
}

@Component({
  selector: 'app-book-edit',
  templateUrl: './book-edit.component.html',
  styleUrls: ['./book-edit.component.scss']
})
export class BookEditComponent implements OnInit {
  id: number;
  bookForm = this.fb.group({
    id: [],
    name: [],
    synonym: [],
    hidden: [],
    cover: []
  });
  book: Book;
  error: Error;

  constructor(public dialogRef: MatDialogRef<BookEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private bookService: BookService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
  }

  static show(dialog: MatDialog, id: number): Observable<any> {
    const dialogRef = dialog.open(BookEditComponent, {
      width: '600px',
      data: { id: id }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getBook();
  }

  private getBook() {
    const getBook$ = !this.id ?
      of(new Book()) :
      this.bookService.getBook({ id: this.id });
    getBook$.subscribe(book => {
      this.book = book;
      this.bookForm.patchValue(this.book);
    });
  }

  onSave(): void {
    this.saveBook();
  }

  private saveBook() {
    const saveBook$ = this.id ?
      this.bookService.updateBook(this.bookForm.value) :
      this.bookService.createBook(this.bookForm.value);
    saveBook$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.bookForm, error);
    }
  }
}
