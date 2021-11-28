import { Component, Input } from '@angular/core';

import { WorkerInfo } from './worker-info.model';

@Component({
  selector: 'app-worker-info-show',
  templateUrl: './worker-info-show.component.html'
})

export class WorkerInfoShowComponent {
  @Input()
  value: WorkerInfo;
}
