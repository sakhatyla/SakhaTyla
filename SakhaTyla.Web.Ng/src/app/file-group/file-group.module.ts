import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { FileGroupCoreModule } from '../file-group-core/file-group-core.module';
import { FileGroupTypeModule } from '../file-group-type/file-group-type.module';

import { FileGroupListComponent } from './file-group-list.component';
import { FileGroupEditComponent } from './file-group-edit.component';
import { FileGroupViewComponent } from './file-group-view.component';
import { FileGroupsComponent } from './file-groups.component';

@NgModule({
  declarations: [
    FileGroupListComponent,
    FileGroupEditComponent,
    FileGroupViewComponent,
    FileGroupsComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    FileGroupTypeModule,
    FileGroupCoreModule
  ],
  exports: [
    FileGroupListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    FileGroupEditComponent
  ]
})
export class FileGroupModule {

}
