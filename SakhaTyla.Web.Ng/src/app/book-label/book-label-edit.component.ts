import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { BookLabel } from '../book-label-core/book-label.model';
import { BookLabelService } from '../book-label-core/book-label.service';

class DialogData {
  id: number;
  bookId?: number;
}

@Component({
  selector: 'app-book-label-edit',
  templateUrl: './book-label-edit.component.html',
  styleUrls: ['./book-label-edit.component.scss']
})
export class BookLabelEditComponent implements OnInit {
  id: number;
  bookId?: number;
  bookLabelForm = this.fb.group({
    id: [],
    bookId: [],
    name: [],
    pageId: []
  });
  bookLabel: BookLabel;
  error: Error;

  constructor(public dialogRef: MatDialogRef<BookLabelEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private bookLabelService: BookLabelService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
    this.bookId = data.bookId;
  }

  static show(dialog: MatDialog, id: number, bookId?: number): Observable<any> {
    const dialogRef = dialog.open(BookLabelEditComponent, {
      width: '600px',
      data: { id: id, bookId: bookId }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getBookLabel();
  }

  private getBookLabel() {
    const getBookLabel$ = !this.id ?
      of(new BookLabel(this.bookId)) :
      this.bookLabelService.getBookLabel({ id: this.id });
    getBookLabel$.subscribe(bookLabel => {
      this.bookLabel = bookLabel;
      this.bookLabelForm.patchValue(this.bookLabel);
    });
  }

  onSave(): void {
    this.saveBookLabel();
  }

  private saveBookLabel() {
    const bookLabel = this.bookLabelForm.value;
    bookLabel.bookId = this.bookId;
    const saveBookLabel$ = this.id ?
      this.bookLabelService.updateBookLabel(bookLabel) :
      this.bookLabelService.createBookLabel(bookLabel);
    saveBookLabel$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.bookLabelForm, error);
    }
  }
}
