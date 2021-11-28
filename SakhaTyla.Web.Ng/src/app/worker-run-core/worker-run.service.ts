import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { WorkerRun } from './worker-run.model';
import { GetWorkerRuns, GetWorkerRun, ExportWorkerRuns,
  CreateWorkerRun, DeleteWorkerRun } from './worker-run-request.model';

@Injectable({ providedIn: 'root' })
export class WorkerRunService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getWorkerRuns(getWorkerRuns: GetWorkerRuns): Observable<Page<WorkerRun>> {
    const url = `${this.apiUrl}/GetWorkerRuns`;
    return this.httpClient.post<Page<WorkerRun>>(url, getWorkerRuns);
  }

  getWorkerRun(getWorkerRun: GetWorkerRun): Observable<WorkerRun> {
    const url = `${this.apiUrl}/GetWorkerRun`;
    return this.httpClient.post<WorkerRun>(url, getWorkerRun);
  }

  exportWorkerRuns(exportWorkerRuns: ExportWorkerRuns): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportWorkerRuns`;
    return this.httpClient.post(url, exportWorkerRuns, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  createWorkerRun(createWorkerRun: CreateWorkerRun): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateWorkerRun`;
    return this.httpClient.post<CreatedEntity<number>>(url, createWorkerRun);
  }

  deleteWorkerRun(deleteWorkerRun: DeleteWorkerRun): Observable<{}> {
    const url = `${this.apiUrl}/DeleteWorkerRun`;
    return this.httpClient.post(url, deleteWorkerRun);
  }
}
