import { Component, Input } from '@angular/core';

import { WorkerRunStatus, WorkerRunStatusDisplay } from './worker-run-status.model';

@Component({
  selector: 'app-worker-run-status-view',
  templateUrl: './worker-run-status-view.component.html'
})

export class WorkerRunStatusViewComponent {
  constructor() { }

  WorkerRunStatusDisplay = WorkerRunStatusDisplay;

  @Input()
  value: WorkerRunStatus;
}
