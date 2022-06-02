import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { Book } from '../book-core/book.model';
import { BookService } from '../book-core/book.service';
import { BookEditComponent } from './book-edit.component';
import { BookPageListState } from '../book-page-core/book-page.model';
import { BookPageFilter } from '../book-page-core/book-page-filter.model';

@Component({
  selector: 'app-book-view',
  templateUrl: './book-view.component.html',
  styleUrls: ['./book-view.component.scss']
})
export class BookViewComponent implements OnInit {
  id: number;
  book: Book;
  bookPageListState: BookPageListState;

  constructor(private dialog: MatDialog,
              private bookService: BookService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getBook();
    });
  }

  private getBook() {
    this.bookService.getBook({ id: this.id })
      .subscribe(book => {
        this.book = book;
        const bookPageFilter = new BookPageFilter();
        bookPageFilter.bookId = book.id;
        this.bookPageListState = new BookPageListState();
        this.bookPageListState.filter = bookPageFilter;
      });
  }

  onEdit() {
    BookEditComponent.show(this.dialog, this.id).subscribe(() => {
      this.getBook();
    });
  }

  onBack(): void {
    window.history.back();
  }
}
