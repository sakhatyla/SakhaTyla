import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FileGroupsComponent } from './file-groups.component';
import { FileGroupViewComponent } from './file-group-view.component';
import { FileGroupModule } from '../file-group/file-group.module';

@NgModule({
    declarations: [
    ],
    imports: [
        RouterModule.forChild([
            { path: '', component: FileGroupsComponent },
            { path: ':id', component: FileGroupViewComponent }
        ]),
        FileGroupModule,
    ],
    providers: [
    ],
    entryComponents: [
    ]
})
export class FileGroupRoutedModule {

}
