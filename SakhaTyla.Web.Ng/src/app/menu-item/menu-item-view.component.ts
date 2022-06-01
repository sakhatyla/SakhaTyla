import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { MenuItem } from '../menu-item-core/menu-item.model';
import { MenuItemService } from '../menu-item-core/menu-item.service';
import { MenuItemEditComponent } from './menu-item-edit.component';

@Component({
  selector: 'app-menu-item-view',
  templateUrl: './menu-item-view.component.html',
  styleUrls: ['./menu-item-view.component.scss']
})
export class MenuItemViewComponent implements OnInit {
  id: number;
  menuItem: MenuItem;

  constructor(private dialog: MatDialog,
              private menuItemService: MenuItemService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getMenuItem();
    });
  }

  private getMenuItem() {
    this.menuItemService.getMenuItem({ id: this.id })
      .subscribe(menuItem => this.menuItem = menuItem);
  }

  onEdit() {
    MenuItemEditComponent.show(this.dialog, this.id, null).subscribe(() => {
      this.getMenuItem();
    });
  }

  onBack(): void {
    window.history.back();
  }
}
