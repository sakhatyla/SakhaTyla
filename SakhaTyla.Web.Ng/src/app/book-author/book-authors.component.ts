import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { BookAuthorListState } from '../book-author-core/book-author.model';

@Component({
  selector: 'app-book-authors',
  templateUrl: './book-authors.component.html',
  styleUrls: ['./book-authors.component.scss']
})
export class BookAuthorsComponent {

  state: BookAuthorListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('bookAuthorListState', new BookAuthorListState());
  }

}
