import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { BookPage } from './book-page.model';
import { GetBookPages, GetBookPage, ExportBookPages,
  UpdateBookPage, CreateBookPage, DeleteBookPage } from './book-page-request.model';

@Injectable({ providedIn: 'root' })
export class BookPageService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getBookPages(getBookPages: GetBookPages): Observable<Page<BookPage>> {
    const url = `${this.apiUrl}/GetBookPages`;
    return this.httpClient.post<Page<BookPage>>(url, getBookPages);
  }

  getBookPage(getBookPage: GetBookPage): Observable<BookPage> {
    const url = `${this.apiUrl}/GetBookPage`;
    return this.httpClient.post<BookPage>(url, getBookPage);
  }

  exportBookPages(exportBookPages: ExportBookPages): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportBookPages`;
    return this.httpClient.post(url, exportBookPages, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updateBookPage(updateBookPage: UpdateBookPage): Observable<{}> {
    const url = `${this.apiUrl}/UpdateBookPage`;
    return this.httpClient.post(url, updateBookPage);
  }

  createBookPage(createBookPage: CreateBookPage): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateBookPage`;
    return this.httpClient.post<CreatedEntity<number>>(url, createBookPage);
  }

  deleteBookPage(deleteBookPage: DeleteBookPage): Observable<{}> {
    const url = `${this.apiUrl}/DeleteBookPage`;
    return this.httpClient.post(url, deleteBookPage);
  }
}
