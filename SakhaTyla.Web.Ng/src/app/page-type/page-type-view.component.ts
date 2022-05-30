import { Component, Input } from '@angular/core';

import { PageType, PageTypeDisplay } from './page-type.model';

@Component({
  selector: 'app-page-type-view',
  templateUrl: './page-type-view.component.html'
})

export class PageTypeViewComponent {
  constructor() { }

  PageTypeDisplay = PageTypeDisplay;

  @Input()
  value: PageType;
}
