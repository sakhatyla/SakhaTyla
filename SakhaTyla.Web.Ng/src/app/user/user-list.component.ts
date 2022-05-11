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

import { User, UserListState } from '../user-core/user.model';
import { UserService } from '../user-core/user.service';
import { UserEditComponent } from './user-edit.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  content: Page<User>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'select',
    'userName',
    'email',
    'emailConfirmed',
    'firstName',
    'lastName',
    'action'
  ];
  selectedIds = new Set<number>();

  @Input()
  state: UserListState = new UserListState();

  @Input()
  baseRoute = '/user';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private userService: UserService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getUsers();
  }

  private getUsers() {
    this.selectedIds = new Set<number>();
    this.userService.getUsers({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getUsers();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.state.filter.roleId = null;
    this.getUsers();
  }

  onCreate() {
    UserEditComponent.show(this.dialog, null).subscribe(() => {
      this.getUsers();
    });
  }

  onExport(): void {
    this.userService.exportUsers({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    UserEditComponent.show(this.dialog, id).subscribe(() => {
      this.getUsers();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.userService.deleteUser({ id }))
      )
      .subscribe(() => this.getUsers(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.userService.deleteUser({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getUsers());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getUsers();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getUsers();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
