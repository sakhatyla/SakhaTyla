import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { BookPage } from '../book-page-core/book-page.model';
import { BookPageService } from '../book-page-core/book-page.service';
import { BookPageEditComponent } from './book-page-edit.component';

@Component({
  selector: 'app-book-page-view',
  templateUrl: './book-page-view.component.html',
  styleUrls: ['./book-page-view.component.scss']
})
export class BookPageViewComponent implements OnInit {
  id: number;
  bookPage: BookPage;

  constructor(private dialog: MatDialog,
              private bookPageService: BookPageService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getBookPage();
    });
  }

  private getBookPage() {
    this.bookPageService.getBookPage({ id: this.id })
      .subscribe(bookPage => this.bookPage = bookPage);
  }

  onEdit() {
    BookPageEditComponent.show(this.dialog, this.id).subscribe(() => {
      this.getBookPage();
    });
  }

  onBack(): void {
    window.history.back();
  }
}
