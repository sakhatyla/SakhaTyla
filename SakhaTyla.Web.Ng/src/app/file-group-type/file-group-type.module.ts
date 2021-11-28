import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { FileGroupTypeSelectComponent } from './file-group-type-select.component';
import { FileGroupTypeViewComponent } from './file-group-type-view.component';

@NgModule({
  declarations: [
    FileGroupTypeSelectComponent,
    FileGroupTypeViewComponent
  ],
  imports: [
    CoreModule
  ],
  providers: [
  ],
  exports: [
    FileGroupTypeSelectComponent,
    FileGroupTypeViewComponent
  ]
})
export class FileGroupTypeModule {

}
