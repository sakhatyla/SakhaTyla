import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { Sort } from '@angular/material/sort';
import { forkJoin, of } from 'rxjs';
import { catchError, mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';
import { OrderDirectionManager } from '../core/models/order-direction.model';

import { MenuItem, MenuItemListState } from '../menu-item-core/menu-item.model';
import { MenuItemService } from '../menu-item-core/menu-item.service';
import { MenuItemEditComponent } from './menu-item-edit.component';

@Component({
  selector: 'app-menu-item-list',
  templateUrl: './menu-item-list.component.html',
  styleUrls: ['./menu-item-list.component.scss']
})
export class MenuItemListComponent implements OnInit {
  menuItems: MenuItem[];

  @Input()
  state: MenuItemListState;

  @Input()
  baseRoute = '/menu-item';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private menuItemService: MenuItemService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getMenuItems();
  }

  private getMenuItems() {
    this.menuItemService.getMenuItems({
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.menuItems = content.pageItems.filter(e => !e.parentId));
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getMenuItems();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getMenuItems();
  }

  onCreate() {
    MenuItemEditComponent.show(this.dialog, null, this.state.filter.menuId).subscribe(() => {
      this.getMenuItems();
    });
  }

  onExport(): void {
    this.menuItemService.exportMenuItems({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    MenuItemEditComponent.show(this.dialog, id, null).subscribe(() => {
      this.getMenuItems();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.menuItemService.deleteMenuItem({ id }))
      )
      .subscribe(() => this.getMenuItems(),
        error => this.onError(error));
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
