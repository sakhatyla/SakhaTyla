import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { WorkerRunListState } from '../worker-run-core/worker-run.model';

@Component({
  selector: 'app-worker-runs',
  templateUrl: './worker-runs.component.html',
  styleUrls: ['./worker-runs.component.scss']
})
export class WorkerRunsComponent {

  state: WorkerRunListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('workerRunListState', new WorkerRunListState());
  }

}
