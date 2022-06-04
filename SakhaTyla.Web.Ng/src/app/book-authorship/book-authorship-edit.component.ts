import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { BookAuthorship } from '../book-authorship-core/book-authorship.model';
import { BookAuthorshipService } from '../book-authorship-core/book-authorship.service';

class DialogData {
  id: number;
  bookId?: number;
}

@Component({
  selector: 'app-book-authorship-edit',
  templateUrl: './book-authorship-edit.component.html',
  styleUrls: ['./book-authorship-edit.component.scss']
})
export class BookAuthorshipEditComponent implements OnInit {
  id: number;
  bookId?: number;
  bookAuthorshipForm = this.fb.group({
    id: [],
    bookId: [],
    authorId: [],
    weight: []
  });
  bookAuthorship: BookAuthorship;
  error: Error;

  constructor(public dialogRef: MatDialogRef<BookAuthorshipEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private bookAuthorshipService: BookAuthorshipService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
    this.bookId = data.bookId;
  }

  static show(dialog: MatDialog, id: number, bookId?: number): Observable<any> {
    const dialogRef = dialog.open(BookAuthorshipEditComponent, {
      width: '600px',
      data: { id: id, bookId: bookId }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getBookAuthorship();
  }

  private getBookAuthorship() {
    const getBookAuthorship$ = !this.id ?
      of(new BookAuthorship()) :
      this.bookAuthorshipService.getBookAuthorship({ id: this.id });
    getBookAuthorship$.subscribe(bookAuthorship => {
      this.bookAuthorship = bookAuthorship;
      this.bookAuthorshipForm.patchValue(this.bookAuthorship);
    });
  }

  onSave(): void {
    this.saveBookAuthorship();
  }

  private saveBookAuthorship() {
    const bookAuthorship = this.bookAuthorshipForm.value;
    bookAuthorship.bookId = this.bookId;
    const saveBookAuthorship$ = this.id ?
      this.bookAuthorshipService.updateBookAuthorship(bookAuthorship) :
      this.bookAuthorshipService.createBookAuthorship(bookAuthorship);
    saveBookAuthorship$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.bookAuthorshipForm, error);
    }
  }
}
