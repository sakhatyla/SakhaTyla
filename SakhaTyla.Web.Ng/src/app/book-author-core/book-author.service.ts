import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { BookAuthor } from './book-author.model';
import { GetBookAuthors, GetBookAuthor, ExportBookAuthors,
  UpdateBookAuthor, CreateBookAuthor, DeleteBookAuthor } from './book-author-request.model';

@Injectable({ providedIn: 'root' })
export class BookAuthorService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getBookAuthors(getBookAuthors: GetBookAuthors): Observable<Page<BookAuthor>> {
    const url = `${this.apiUrl}/GetBookAuthors`;
    return this.httpClient.post<Page<BookAuthor>>(url, getBookAuthors);
  }

  getBookAuthor(getBookAuthor: GetBookAuthor): Observable<BookAuthor> {
    const url = `${this.apiUrl}/GetBookAuthor`;
    return this.httpClient.post<BookAuthor>(url, getBookAuthor);
  }

  exportBookAuthors(exportBookAuthors: ExportBookAuthors): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportBookAuthors`;
    return this.httpClient.post(url, exportBookAuthors, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updateBookAuthor(updateBookAuthor: UpdateBookAuthor): Observable<{}> {
    const url = `${this.apiUrl}/UpdateBookAuthor`;
    return this.httpClient.post(url, updateBookAuthor);
  }

  createBookAuthor(createBookAuthor: CreateBookAuthor): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateBookAuthor`;
    return this.httpClient.post<CreatedEntity<number>>(url, createBookAuthor);
  }

  deleteBookAuthor(deleteBookAuthor: DeleteBookAuthor): Observable<{}> {
    const url = `${this.apiUrl}/DeleteBookAuthor`;
    return this.httpClient.post(url, deleteBookAuthor);
  }
}
