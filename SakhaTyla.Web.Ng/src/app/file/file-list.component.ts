import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';

import { File, FileListState } from '../file-core/file.model';
import { FileService } from '../file-core/file.service';
import { FileEditComponent } from './file-edit.component';

@Component({
    selector: 'app-file-list',
    templateUrl: './file-list.component.html',
    styleUrls: ['./file-list.component.scss']
})
export class FileListComponent implements OnInit {
    content: Page<File>;
    pageSizeOptions = [10, 20];
    columns = [
        'name',
        'contentType',
        'url',
        'group',
        'action'
    ];

    @Input()
    state: FileListState;

    @Input()
    baseRoute = '/file';

    constructor(
        private dialog: MatDialog,
        private modalHelper: ModalHelper,
        private fileService: FileService,
        private noticeHelper: NoticeHelper
        ) {
    }

    ngOnInit() {
        this.getFiles();
    }

    private getFiles() {
        this.fileService.getFiles({
            pageIndex: this.state.pageIndex,
            pageSize: this.state.pageSize,
            filter: this.state.filter
        }).subscribe(content => this.content = content);
    }

    onSearch() {
        this.getFiles();
    }

    onReset() {
        this.state.filter.text = null;
        this.getFiles();
    }

    onCreate() {
        FileEditComponent.show(this.dialog, null).subscribe(() => {
            this.getFiles();
        });
    }

    onExport(): void {
        this.fileService.exportFiles({ filter: this.state.filter })
            .subscribe(file => {
                file.download();
            });
    }

    onEdit(id: number) {
        FileEditComponent.show(this.dialog, id).subscribe(() => {
            this.getFiles();
        });
    }

    onDelete(id: number) {
        this.modalHelper.confirmDelete()
            .pipe(
                mergeMap(() => this.fileService.deleteFile({ id }))
            )
            .subscribe(() => this.getFiles(),
                error => this.onError(error));
    }

    onPage(page: PageEvent) {
        this.state.pageIndex = page.pageIndex;
        this.state.pageSize = page.pageSize;
        this.getFiles();
    }

    onError(error: Error) {
        if (error) {
            this.noticeHelper.showError(error);
        }
    }
}
