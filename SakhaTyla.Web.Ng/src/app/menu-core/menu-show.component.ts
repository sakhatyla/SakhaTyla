import { Component, Input } from '@angular/core';

import { Menu } from './menu.model';

@Component({
  selector: 'app-menu-show',
  templateUrl: './menu-show.component.html'
})

export class MenuShowComponent {
  @Input()
  value: Menu;
}
