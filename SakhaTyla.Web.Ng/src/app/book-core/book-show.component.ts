import { Component, Input } from '@angular/core';

import { Book } from './book.model';

@Component({
  selector: 'app-book-show',
  templateUrl: './book-show.component.html'
})

export class BookShowComponent {
  @Input()
  value: Book;
}
