import { Component, Input } from '@angular/core';

import { BookAuthor } from './book-author.model';

@Component({
  selector: 'app-book-author-show',
  templateUrl: './book-author-show.component.html'
})

export class BookAuthorShowComponent {
  @Input()
  value: BookAuthor;
}
