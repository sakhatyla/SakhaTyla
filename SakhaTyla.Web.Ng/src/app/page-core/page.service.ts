import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page as PageModel } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { Page } from './page.model';
import { GetPages, GetPage, ExportPages,
  UpdatePage, CreatePage, DeletePage } from './page-request.model';

@Injectable({ providedIn: 'root' })
export class PageService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getPages(getPages: GetPages): Observable<PageModel<Page>> {
    const url = `${this.apiUrl}/GetPages`;
    return this.httpClient.post<PageModel<Page>>(url, getPages);
  }

  getPage(getPage: GetPage): Observable<Page> {
    const url = `${this.apiUrl}/GetPage`;
    return this.httpClient.post<Page>(url, getPage);
  }

  exportPages(exportPages: ExportPages): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportPages`;
    return this.httpClient.post(url, exportPages, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updatePage(updatePage: UpdatePage): Observable<{}> {
    const url = `${this.apiUrl}/UpdatePage`;
    return this.httpClient.post(url, updatePage);
  }

  createPage(createPage: CreatePage): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreatePage`;
    return this.httpClient.post<CreatedEntity<number>>(url, createPage);
  }

  deletePage(deletePage: DeletePage): Observable<{}> {
    const url = `${this.apiUrl}/DeletePage`;
    return this.httpClient.post(url, deletePage);
  }
}
