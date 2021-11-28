import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { WorkerInfosComponent } from './worker-infos.component';
import { WorkerInfoViewComponent } from './worker-info-view.component';
import { WorkerInfoModule } from '../worker-info/worker-info.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: WorkerInfosComponent },
      { path: ':id', component: WorkerInfoViewComponent }
    ]),
    WorkerInfoModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class WorkerInfoRoutedModule {

}
