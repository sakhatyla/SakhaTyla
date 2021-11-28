import { Component, Input } from '@angular/core';

import { WorkerRun } from './worker-run.model';

@Component({
  selector: 'app-worker-run-show',
  templateUrl: './worker-run-show.component.html'
})

export class WorkerRunShowComponent {
  @Input()
  value: WorkerRun;
}
