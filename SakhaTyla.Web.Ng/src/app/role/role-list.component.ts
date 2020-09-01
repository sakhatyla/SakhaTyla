import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';

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
    pageSizeOptions = [10, 20];
    columns = [
        'name',
        'displayName',
        'action'
    ];

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
        this.roleService.getRoles({
            pageIndex: this.state.pageIndex,
            pageSize: this.state.pageSize,
            filter: this.state.filter
        }).subscribe(content => this.content = content);
    }

    onSearch() {
        this.getRoles();
    }

    onReset() {
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

    onPage(page: PageEvent) {
        this.state.pageIndex = page.pageIndex;
        this.state.pageSize = page.pageSize;
        this.getRoles();
    }

    onError(error: Error) {
        if (error) {
            this.noticeHelper.showError(error);
        }
    }
}
