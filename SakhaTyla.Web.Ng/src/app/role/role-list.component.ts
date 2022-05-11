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

import { Role, RoleListState } from '../role-core/role.model';
import { RoleService } from '../role-core/role.service';
import { RoleEditComponent } from './role-edit.component';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.scss']
})
export class RoleListComponent implements OnInit {
  content: Page<Role>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'select',
    'name',
    'displayName',
    'action'
  ];
  selectedIds = new Set<number>();

  @Input()
  state: RoleListState = new RoleListState();

  @Input()
  baseRoute = '/role';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private roleService: RoleService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getRoles();
  }

  private getRoles() {
    this.selectedIds = new Set<number>();
    this.roleService.getRoles({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getRoles();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getRoles();
  }

  onCreate() {
    RoleEditComponent.show(this.dialog, null).subscribe(() => {
      this.getRoles();
    });
  }

  onExport(): void {
    this.roleService.exportRoles({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    RoleEditComponent.show(this.dialog, id).subscribe(() => {
      this.getRoles();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.roleService.deleteRole({ id }))
      )
      .subscribe(() => this.getRoles(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.roleService.deleteRole({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getRoles());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getRoles();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getRoles();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
