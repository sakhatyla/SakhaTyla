import { Component, Input } from '@angular/core';

import { File } from './file.model';

@Component({
    selector: 'app-file-show',
    templateUrl: './file-show.component.html'
})

export class FileShowComponent {
    @Input()
    value: File;
}
