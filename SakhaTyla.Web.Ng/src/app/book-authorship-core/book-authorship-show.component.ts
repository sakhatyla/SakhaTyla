import { Component, Input } from '@angular/core';

import { BookAuthorship } from './book-authorship.model';

@Component({
  selector: 'app-book-authorship-show',
  templateUrl: './book-authorship-show.component.html'
})

export class BookAuthorshipShowComponent {
  @Input()
  value: BookAuthorship;
}
