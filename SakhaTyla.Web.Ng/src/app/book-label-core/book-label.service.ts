import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { BookLabel } from './book-label.model';
import { GetBookLabels, GetBookLabel, ExportBookLabels,
  UpdateBookLabel, CreateBookLabel, DeleteBookLabel } from './book-label-request.model';

@Injectable({ providedIn: 'root' })
export class BookLabelService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getBookLabels(getBookLabels: GetBookLabels): Observable<Page<BookLabel>> {
    const url = `${this.apiUrl}/GetBookLabels`;
    return this.httpClient.post<Page<BookLabel>>(url, getBookLabels);
  }

  getBookLabel(getBookLabel: GetBookLabel): Observable<BookLabel> {
    const url = `${this.apiUrl}/GetBookLabel`;
    return this.httpClient.post<BookLabel>(url, getBookLabel);
  }

  exportBookLabels(exportBookLabels: ExportBookLabels): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportBookLabels`;
    return this.httpClient.post(url, exportBookLabels, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updateBookLabel(updateBookLabel: UpdateBookLabel): Observable<{}> {
    const url = `${this.apiUrl}/UpdateBookLabel`;
    return this.httpClient.post(url, updateBookLabel);
  }

  createBookLabel(createBookLabel: CreateBookLabel): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateBookLabel`;
    return this.httpClient.post<CreatedEntity<number>>(url, createBookLabel);
  }

  deleteBookLabel(deleteBookLabel: DeleteBookLabel): Observable<{}> {
    const url = `${this.apiUrl}/DeleteBookLabel`;
    return this.httpClient.post(url, deleteBookLabel);
  }
}
