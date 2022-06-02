import { Component, Input } from '@angular/core';

import { BookPage } from './book-page.model';

@Component({
  selector: 'app-book-page-show',
  templateUrl: './book-page-show.component.html'
})

export class BookPageShowComponent {
  @Input()
  value: BookPage;
}
