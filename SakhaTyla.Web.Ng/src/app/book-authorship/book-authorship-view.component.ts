import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { BookAuthorship } from '../book-authorship-core/book-authorship.model';
import { BookAuthorshipService } from '../book-authorship-core/book-authorship.service';
import { BookAuthorshipEditComponent } from './book-authorship-edit.component';

@Component({
  selector: 'app-book-authorship-view',
  templateUrl: './book-authorship-view.component.html',
  styleUrls: ['./book-authorship-view.component.scss']
})
export class BookAuthorshipViewComponent implements OnInit {
  id: number;
  bookAuthorship: BookAuthorship;

  constructor(private dialog: MatDialog,
              private bookAuthorshipService: BookAuthorshipService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getBookAuthorship();
    });
  }

  private getBookAuthorship() {
    this.bookAuthorshipService.getBookAuthorship({ id: this.id })
      .subscribe(bookAuthorship => this.bookAuthorship = bookAuthorship);
  }

  onEdit() {
    BookAuthorshipEditComponent.show(this.dialog, this.id).subscribe(() => {
      this.getBookAuthorship();
    });
  }

  onBack(): void {
    window.history.back();
  }
}
