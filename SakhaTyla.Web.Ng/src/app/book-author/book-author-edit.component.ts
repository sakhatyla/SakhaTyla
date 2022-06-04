import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { BookAuthor } from '../book-author-core/book-author.model';
import { BookAuthorService } from '../book-author-core/book-author.service';

class DialogData {
  id: number;
}

@Component({
  selector: 'app-book-author-edit',
  templateUrl: './book-author-edit.component.html',
  styleUrls: ['./book-author-edit.component.scss']
})
export class BookAuthorEditComponent implements OnInit {
  id: number;
  bookAuthorForm = this.fb.group({
    id: [],
    lastName: [],
    firstName: [],
    middleName: [],
    nickName: []
  });
  bookAuthor: BookAuthor;
  error: Error;

  constructor(public dialogRef: MatDialogRef<BookAuthorEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private bookAuthorService: BookAuthorService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
  }

  static show(dialog: MatDialog, id: number): Observable<any> {
    const dialogRef = dialog.open(BookAuthorEditComponent, {
      width: '600px',
      data: { id: id }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getBookAuthor();
  }

  private getBookAuthor() {
    const getBookAuthor$ = !this.id ?
      of(new BookAuthor()) :
      this.bookAuthorService.getBookAuthor({ id: this.id });
    getBookAuthor$.subscribe(bookAuthor => {
      this.bookAuthor = bookAuthor;
      this.bookAuthorForm.patchValue(this.bookAuthor);
    });
  }

  onSave(): void {
    this.saveBookAuthor();
  }

  private saveBookAuthor() {
    const saveBookAuthor$ = this.id ?
      this.bookAuthorService.updateBookAuthor(this.bookAuthorForm.value) :
      this.bookAuthorService.createBookAuthor(this.bookAuthorForm.value);
    saveBookAuthor$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.bookAuthorForm, error);
    }
  }
}
