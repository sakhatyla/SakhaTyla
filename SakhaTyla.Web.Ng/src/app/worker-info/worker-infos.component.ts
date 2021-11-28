import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { WorkerInfoListState } from '../worker-info-core/worker-info.model';

@Component({
  selector: 'app-worker-infos',
  templateUrl: './worker-infos.component.html',
  styleUrls: ['./worker-infos.component.scss']
})
export class WorkerInfosComponent {

  state: WorkerInfoListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('workerInfoListState', new WorkerInfoListState());
  }

}
