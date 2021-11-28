import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FilesComponent } from './files.component';
import { FileViewComponent } from './file-view.component';
import { FileModule } from '../file/file.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: FilesComponent },
      { path: ':id', component: FileViewComponent }
    ]),
    FileModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class FileRoutedModule {

}
