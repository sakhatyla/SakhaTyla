import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { WorkerInfo } from './worker-info.model';
import { GetWorkerInfos, GetWorkerInfo, ExportWorkerInfos,
  UpdateWorkerInfo, CreateWorkerInfo, DeleteWorkerInfo } from './worker-info-request.model';

@Injectable({ providedIn: 'root' })
export class WorkerInfoService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getWorkerInfos(getWorkerInfos: GetWorkerInfos): Observable<Page<WorkerInfo>> {
    const url = `${this.apiUrl}/GetWorkerInfos`;
    return this.httpClient.post<Page<WorkerInfo>>(url, getWorkerInfos);
  }

  getWorkerInfo(getWorkerInfo: GetWorkerInfo): Observable<WorkerInfo> {
    const url = `${this.apiUrl}/GetWorkerInfo`;
    return this.httpClient.post<WorkerInfo>(url, getWorkerInfo);
  }

  exportWorkerInfos(exportWorkerInfos: ExportWorkerInfos): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportWorkerInfos`;
    return this.httpClient.post(url, exportWorkerInfos, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updateWorkerInfo(updateWorkerInfo: UpdateWorkerInfo): Observable<{}> {
    const url = `${this.apiUrl}/UpdateWorkerInfo`;
    return this.httpClient.post(url, updateWorkerInfo);
  }

  createWorkerInfo(createWorkerInfo: CreateWorkerInfo): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateWorkerInfo`;
    return this.httpClient.post<CreatedEntity<number>>(url, createWorkerInfo);
  }

  deleteWorkerInfo(deleteWorkerInfo: DeleteWorkerInfo): Observable<{}> {
    const url = `${this.apiUrl}/DeleteWorkerInfo`;
    return this.httpClient.post(url, deleteWorkerInfo);
  }
}
