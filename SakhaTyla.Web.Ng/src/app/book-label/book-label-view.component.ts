import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { BookLabel } from '../book-label-core/book-label.model';
import { BookLabelService } from '../book-label-core/book-label.service';
import { BookLabelEditComponent } from './book-label-edit.component';

@Component({
  selector: 'app-book-label-view',
  templateUrl: './book-label-view.component.html',
  styleUrls: ['./book-label-view.component.scss']
})
export class BookLabelViewComponent implements OnInit {
  id: number;
  bookLabel: BookLabel;

  constructor(private dialog: MatDialog,
              private bookLabelService: BookLabelService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getBookLabel();
    });
  }

  private getBookLabel() {
    this.bookLabelService.getBookLabel({ id: this.id })
      .subscribe(bookLabel => this.bookLabel = bookLabel);
  }

  onEdit() {
    BookLabelEditComponent.show(this.dialog, this.id).subscribe(() => {
      this.getBookLabel();
    });
  }

  onBack(): void {
    window.history.back();
  }
}
