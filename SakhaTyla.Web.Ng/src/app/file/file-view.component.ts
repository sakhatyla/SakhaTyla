import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { File } from '../file-core/file.model';
import { FileService } from '../file-core/file.service';
import { FileEditComponent } from './file-edit.component';

@Component({
    selector: 'app-file-view',
    templateUrl: './file-view.component.html',
    styleUrls: ['./file-view.component.scss']
})
export class FileViewComponent implements OnInit {
    id: number;
    file: File;

    constructor(private dialog: MatDialog,
                private fileService: FileService,
                private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.route.params.forEach((params: Params) => {
            this.id = ConvertStringTo.number(params.id);
            this.getFile();
        });
    }

    private getFile() {
        this.fileService.getFile({ id: this.id })
            .subscribe(file => this.file = file);
    }

    onEdit() {
        FileEditComponent.show(this.dialog, this.id).subscribe(() => {
            this.getFile();
        });
    }

    onBack(): void {
        window.history.back();
    }

    onDownload(): void {
        this.fileService.downloadFile({ id: this.id })
            .subscribe(file => {
                file.download();
            });
    }
}
