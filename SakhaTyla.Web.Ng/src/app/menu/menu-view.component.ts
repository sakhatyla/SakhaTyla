import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { Menu } from '../menu-core/menu.model';
import { MenuService } from '../menu-core/menu.service';
import { MenuEditComponent } from './menu-edit.component';

@Component({
  selector: 'app-menu-view',
  templateUrl: './menu-view.component.html',
  styleUrls: ['./menu-view.component.scss']
})
export class MenuViewComponent implements OnInit {
  id: number;
  menu: Menu;

  constructor(private dialog: MatDialog,
              private menuService: MenuService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getMenu();
    });
  }

  private getMenu() {
    this.menuService.getMenu({ id: this.id })
      .subscribe(menu => this.menu = menu);
  }

  onEdit() {
    MenuEditComponent.show(this.dialog, this.id).subscribe(() => {
      this.getMenu();
    });
  }

  onBack(): void {
    window.history.back();
  }
}
