import { Component, Input } from '@angular/core';

import { BookLabel } from './book-label.model';

@Component({
  selector: 'app-book-label-show',
  templateUrl: './book-label-show.component.html'
})

export class BookLabelShowComponent {
  @Input()
  value: BookLabel;
}
