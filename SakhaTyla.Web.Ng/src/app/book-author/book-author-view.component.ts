import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { BookAuthor } from '../book-author-core/book-author.model';
import { BookAuthorService } from '../book-author-core/book-author.service';
import { BookAuthorEditComponent } from './book-author-edit.component';

@Component({
  selector: 'app-book-author-view',
  templateUrl: './book-author-view.component.html',
  styleUrls: ['./book-author-view.component.scss']
})
export class BookAuthorViewComponent implements OnInit {
  id: number;
  bookAuthor: BookAuthor;

  constructor(private dialog: MatDialog,
              private bookAuthorService: BookAuthorService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getBookAuthor();
    });
  }

  private getBookAuthor() {
    this.bookAuthorService.getBookAuthor({ id: this.id })
      .subscribe(bookAuthor => this.bookAuthor = bookAuthor);
  }

  onEdit() {
    BookAuthorEditComponent.show(this.dialog, this.id).subscribe(() => {
      this.getBookAuthor();
    });
  }

  onBack(): void {
    window.history.back();
  }
}
