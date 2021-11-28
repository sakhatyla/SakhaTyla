import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { WorkerRunsComponent } from './worker-runs.component';
import { WorkerRunViewComponent } from './worker-run-view.component';
import { WorkerRunModule } from '../worker-run/worker-run.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: WorkerRunsComponent },
      { path: ':id', component: WorkerRunViewComponent }
    ]),
    WorkerRunModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class WorkerRunRoutedModule {

}
