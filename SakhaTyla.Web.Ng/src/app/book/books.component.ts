import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { BookListState } from '../book-core/book.model';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent {

  state: BookListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('bookListState', new BookListState());
  }

}
