import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { WorkerScheduleTask } from './worker-schedule-task.model';
import { GetWorkerScheduleTasks, GetWorkerScheduleTask, ExportWorkerScheduleTasks,
  UpdateWorkerScheduleTask, CreateWorkerScheduleTask, DeleteWorkerScheduleTask } from './worker-schedule-task-request.model';

@Injectable({ providedIn: 'root' })
export class WorkerScheduleTaskService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getWorkerScheduleTasks(getWorkerScheduleTasks: GetWorkerScheduleTasks): Observable<Page<WorkerScheduleTask>> {
    const url = `${this.apiUrl}/GetWorkerScheduleTasks`;
    return this.httpClient.post<Page<WorkerScheduleTask>>(url, getWorkerScheduleTasks);
  }

  getWorkerScheduleTask(getWorkerScheduleTask: GetWorkerScheduleTask): Observable<WorkerScheduleTask> {
    const url = `${this.apiUrl}/GetWorkerScheduleTask`;
    return this.httpClient.post<WorkerScheduleTask>(url, getWorkerScheduleTask);
  }

  exportWorkerScheduleTasks(exportWorkerScheduleTasks: ExportWorkerScheduleTasks): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportWorkerScheduleTasks`;
    return this.httpClient.post(url, exportWorkerScheduleTasks, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updateWorkerScheduleTask(updateWorkerScheduleTask: UpdateWorkerScheduleTask): Observable<{}> {
    const url = `${this.apiUrl}/UpdateWorkerScheduleTask`;
    return this.httpClient.post(url, updateWorkerScheduleTask);
  }

  createWorkerScheduleTask(createWorkerScheduleTask: CreateWorkerScheduleTask): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateWorkerScheduleTask`;
    return this.httpClient.post<CreatedEntity<number>>(url, createWorkerScheduleTask);
  }

  deleteWorkerScheduleTask(deleteWorkerScheduleTask: DeleteWorkerScheduleTask): Observable<{}> {
    const url = `${this.apiUrl}/DeleteWorkerScheduleTask`;
    return this.httpClient.post(url, deleteWorkerScheduleTask);
  }
}
