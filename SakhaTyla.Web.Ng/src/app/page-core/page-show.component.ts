import { Component, Input } from '@angular/core';

import { Page } from './page.model';

@Component({
  selector: 'app-page-show',
  templateUrl: './page-show.component.html'
})

export class PageShowComponent {
  @Input()
  value: Page;
}
