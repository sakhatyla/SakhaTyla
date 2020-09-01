import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';

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
    pageSizeOptions = [10, 20];
    columns = [
        'userName',
        'email',
        'emailConfirmed',
        'firstName',
        'lastName',
        'action'
    ];

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
        this.userService.getUsers({
            pageIndex: this.state.pageIndex,
            pageSize: this.state.pageSize,
            filter: this.state.filter
        }).subscribe(content => this.content = content);
    }

    onSearch() {
        this.getUsers();
    }

    onReset() {
        this.state.filter.text = null;
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

    onPage(page: PageEvent) {
        this.state.pageIndex = page.pageIndex;
        this.state.pageSize = page.pageSize;
        this.getUsers();
    }

    onError(error: Error) {
        if (error) {
            this.noticeHelper.showError(error);
        }
    }
}
