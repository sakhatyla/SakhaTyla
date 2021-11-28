import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { FileService } from './file.service';
import { FileSelectComponent } from './file-select.component';
import { FileShowComponent } from './file-show.component';
import { TranslocoRootModule } from '../transloco-root.module';

@NgModule({
  declarations: [
    FileSelectComponent,
    FileShowComponent
  ],
  imports: [
    CoreModule,
    TranslocoRootModule,
  ],
  providers: [
    FileService
  ],
  exports: [
    FileSelectComponent,
    FileShowComponent
  ]
})
export class FileCoreModule {

}
