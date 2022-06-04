import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { BookAuthorship } from './book-authorship.model';
import { GetBookAuthorships, GetBookAuthorship, ExportBookAuthorships,
  UpdateBookAuthorship, CreateBookAuthorship, DeleteBookAuthorship } from './book-authorship-request.model';

@Injectable({ providedIn: 'root' })
export class BookAuthorshipService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getBookAuthorships(getBookAuthorships: GetBookAuthorships): Observable<Page<BookAuthorship>> {
    const url = `${this.apiUrl}/GetBookAuthorships`;
    return this.httpClient.post<Page<BookAuthorship>>(url, getBookAuthorships);
  }

  getBookAuthorship(getBookAuthorship: GetBookAuthorship): Observable<BookAuthorship> {
    const url = `${this.apiUrl}/GetBookAuthorship`;
    return this.httpClient.post<BookAuthorship>(url, getBookAuthorship);
  }

  exportBookAuthorships(exportBookAuthorships: ExportBookAuthorships): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportBookAuthorships`;
    return this.httpClient.post(url, exportBookAuthorships, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updateBookAuthorship(updateBookAuthorship: UpdateBookAuthorship): Observable<{}> {
    const url = `${this.apiUrl}/UpdateBookAuthorship`;
    return this.httpClient.post(url, updateBookAuthorship);
  }

  createBookAuthorship(createBookAuthorship: CreateBookAuthorship): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateBookAuthorship`;
    return this.httpClient.post<CreatedEntity<number>>(url, createBookAuthorship);
  }

  deleteBookAuthorship(deleteBookAuthorship: DeleteBookAuthorship): Observable<{}> {
    const url = `${this.apiUrl}/DeleteBookAuthorship`;
    return this.httpClient.post(url, deleteBookAuthorship);
  }
}
