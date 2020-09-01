import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { FileCoreModule } from '../file-core/file-core.module';
import { FileGroupCoreModule } from '../file-group-core/file-group-core.module';

import { FileListComponent } from './file-list.component';
import { FileEditComponent } from './file-edit.component';
import { FileViewComponent } from './file-view.component';
import { FilesComponent } from './files.component';

@NgModule({
    declarations: [
        FileListComponent,
        FileEditComponent,
        FileViewComponent,
        FilesComponent,
    ],
    imports: [
        RouterModule,
        CoreModule,
        TranslocoRootModule,
        FileGroupCoreModule,
        FileCoreModule
    ],
    exports: [
        FileListComponent,
    ],
    providers: [
    ],
    entryComponents: [
        FileEditComponent
    ]
})
export class FileModule {

}
