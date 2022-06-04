import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { BookLabelListState } from '../book-label-core/book-label.model';

@Component({
  selector: 'app-book-labels',
  templateUrl: './book-labels.component.html',
  styleUrls: ['./book-labels.component.scss']
})
export class BookLabelsComponent {

  state: BookLabelListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('bookLabelListState', new BookLabelListState());
  }

}
