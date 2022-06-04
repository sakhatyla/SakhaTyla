import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { BookAuthorshipListState } from '../book-authorship-core/book-authorship.model';

@Component({
  selector: 'app-book-authorships',
  templateUrl: './book-authorships.component.html',
  styleUrls: ['./book-authorships.component.scss']
})
export class BookAuthorshipsComponent {

  state: BookAuthorshipListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('bookAuthorshipListState', new BookAuthorshipListState());
  }

}
