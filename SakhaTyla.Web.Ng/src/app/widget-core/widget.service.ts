import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { Widget } from './widget.model';
import { GetWidgets, GetWidget, ExportWidgets,
  UpdateWidget, CreateWidget, DeleteWidget } from './widget-request.model';

@Injectable({ providedIn: 'root' })
export class WidgetService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getWidgets(getWidgets: GetWidgets): Observable<Page<Widget>> {
    const url = `${this.apiUrl}/GetWidgets`;
    return this.httpClient.post<Page<Widget>>(url, getWidgets);
  }

  getWidget(getWidget: GetWidget): Observable<Widget> {
    const url = `${this.apiUrl}/GetWidget`;
    return this.httpClient.post<Widget>(url, getWidget);
  }

  exportWidgets(exportWidgets: ExportWidgets): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportWidgets`;
    return this.httpClient.post(url, exportWidgets, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updateWidget(updateWidget: UpdateWidget): Observable<{}> {
    const url = `${this.apiUrl}/UpdateWidget`;
    return this.httpClient.post(url, updateWidget);
  }

  createWidget(createWidget: CreateWidget): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateWidget`;
    return this.httpClient.post<CreatedEntity<number>>(url, createWidget);
  }

  deleteWidget(deleteWidget: DeleteWidget): Observable<{}> {
    const url = `${this.apiUrl}/DeleteWidget`;
    return this.httpClient.post(url, deleteWidget);
  }
}
