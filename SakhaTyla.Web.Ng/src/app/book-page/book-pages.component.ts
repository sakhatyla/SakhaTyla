import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { BookPageListState } from '../book-page-core/book-page.model';

@Component({
  selector: 'app-book-pages',
  templateUrl: './book-pages.component.html',
  styleUrls: ['./book-pages.component.scss']
})
export class BookPagesComponent {

  state: BookPageListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('bookPageListState', new BookPageListState());
  }

}
