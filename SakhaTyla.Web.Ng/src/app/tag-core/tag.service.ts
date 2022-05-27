import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { Tag } from './tag.model';
import { GetTags, GetTag, ExportTags,
  UpdateTag, CreateTag, DeleteTag } from './tag-request.model';

@Injectable({ providedIn: 'root' })
export class TagService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getTags(getTags: GetTags): Observable<Page<Tag>> {
    const url = `${this.apiUrl}/GetTags`;
    return this.httpClient.post<Page<Tag>>(url, getTags);
  }

  getTag(getTag: GetTag): Observable<Tag> {
    const url = `${this.apiUrl}/GetTag`;
    return this.httpClient.post<Tag>(url, getTag);
  }

  exportTags(exportTags: ExportTags): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportTags`;
    return this.httpClient.post(url, exportTags, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updateTag(updateTag: UpdateTag): Observable<{}> {
    const url = `${this.apiUrl}/UpdateTag`;
    return this.httpClient.post(url, updateTag);
  }

  createTag(createTag: CreateTag): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateTag`;
    return this.httpClient.post<CreatedEntity<number>>(url, createTag);
  }

  deleteTag(deleteTag: DeleteTag): Observable<{}> {
    const url = `${this.apiUrl}/DeleteTag`;
    return this.httpClient.post(url, deleteTag);
  }
}
