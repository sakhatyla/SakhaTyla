import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { FileListState } from '../file-core/file.model';

@Component({
    selector: 'app-files',
    templateUrl: './files.component.html',
    styleUrls: ['./files.component.scss']
})
export class FilesComponent {

    state: FileListState;

    constructor(private storeService: StoreService) {
        this.state = this.storeService.get('fileListState', new FileListState());
    }

}
