import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { MenuItemListState } from '../menu-item-core/menu-item.model';

@Component({
  selector: 'app-menu-items',
  templateUrl: './menu-items.component.html',
  styleUrls: ['./menu-items.component.scss']
})
export class MenuItemsComponent {

  state: MenuItemListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('menuItemListState', new MenuItemListState());
  }

}
