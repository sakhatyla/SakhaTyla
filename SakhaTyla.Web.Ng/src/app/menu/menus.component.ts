import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { MenuListState } from '../menu-core/menu.model';

@Component({
  selector: 'app-menus',
  templateUrl: './menus.component.html',
  styleUrls: ['./menus.component.scss']
})
export class MenusComponent {

  state: MenuListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('menuListState', new MenuListState());
  }

}
