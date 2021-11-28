import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { FileGroupService } from './file-group.service';
import { FileGroupSelectComponent } from './file-group-select.component';
import { FileGroupShowComponent } from './file-group-show.component';

@NgModule({
  declarations: [
    FileGroupSelectComponent,
    FileGroupShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    FileGroupService
  ],
  exports: [
    FileGroupSelectComponent,
    FileGroupShowComponent
  ]
})
export class FileGroupCoreModule {

}
