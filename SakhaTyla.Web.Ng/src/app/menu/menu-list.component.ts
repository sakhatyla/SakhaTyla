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

import { Menu, MenuListState } from '../menu-core/menu.model';
import { MenuService } from '../menu-core/menu.service';
import { MenuEditComponent } from './menu-edit.component';

@Component({
  selector: 'app-menu-list',
  templateUrl: './menu-list.component.html',
  styleUrls: ['./menu-list.component.scss']
})
export class MenuListComponent implements OnInit {
  content: Page<Menu>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'select',
    'name',
    'code',
    'action'
  ];
  selectedIds = new Set<number>();

  @Input()
  state: MenuListState;

  @Input()
  baseRoute = '/menu';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private menuService: MenuService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getMenus();
  }

  private getMenus() {
    this.selectedIds = new Set<number>();
    this.menuService.getMenus({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getMenus();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getMenus();
  }

  onCreate() {
    MenuEditComponent.show(this.dialog, null).subscribe(() => {
      this.getMenus();
    });
  }

  onExport(): void {
    this.menuService.exportMenus({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    MenuEditComponent.show(this.dialog, id).subscribe(() => {
      this.getMenus();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.menuService.deleteMenu({ id }))
      )
      .subscribe(() => this.getMenus(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.menuService.deleteMenu({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getMenus());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getMenus();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getMenus();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
