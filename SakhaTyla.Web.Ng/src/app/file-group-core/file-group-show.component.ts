import { Component, Input } from '@angular/core';

import { FileGroup } from './file-group.model';

@Component({
    selector: 'app-file-group-show',
    templateUrl: './file-group-show.component.html'
})

export class FileGroupShowComponent {
    @Input()
    value: FileGroup;
}
