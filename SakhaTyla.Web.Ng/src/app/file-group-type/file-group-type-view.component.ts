import { Component, Input } from '@angular/core';

import { FileGroupType, FileGroupTypeDisplay } from './file-group-type.model';

@Component({
    selector: 'app-file-group-type-view',
    templateUrl: './file-group-type-view.component.html'
})

export class FileGroupTypeViewComponent {
    constructor() { }

    FileGroupTypeDisplay = FileGroupTypeDisplay;

    @Input()
    value: FileGroupType;
}
