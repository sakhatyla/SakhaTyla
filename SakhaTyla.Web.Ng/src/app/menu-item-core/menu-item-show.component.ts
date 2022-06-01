import { Component, Input } from '@angular/core';

import { MenuItem } from './menu-item.model';

@Component({
  selector: 'app-menu-item-show',
  templateUrl: './menu-item-show.component.html'
})

export class MenuItemShowComponent {
  @Input()
  value: MenuItem;
}
