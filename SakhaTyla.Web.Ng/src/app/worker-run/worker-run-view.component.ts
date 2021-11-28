import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { WorkerRun } from '../worker-run-core/worker-run.model';
import { WorkerRunService } from '../worker-run-core/worker-run.service';
import { WorkerRunEditComponent } from './worker-run-edit.component';

@Component({
  selector: 'app-worker-run-view',
  templateUrl: './worker-run-view.component.html',
  styleUrls: ['./worker-run-view.component.scss']
})
export class WorkerRunViewComponent implements OnInit {
  id: number;
  workerRun: WorkerRun;

  constructor(private dialog: MatDialog,
              private workerRunService: WorkerRunService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getWorkerRun();
    });
  }

  private getWorkerRun() {
    this.workerRunService.getWorkerRun({ id: this.id })
      .subscribe(workerRun => this.workerRun = workerRun);
  }

  onBack(): void {
    window.history.back();
  }
}
