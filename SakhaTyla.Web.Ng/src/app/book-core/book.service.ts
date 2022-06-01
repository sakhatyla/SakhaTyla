import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { Book } from './book.model';
import { GetBooks, GetBook, ExportBooks,
  UpdateBook, CreateBook, DeleteBook } from './book-request.model';

@Injectable({ providedIn: 'root' })
export class BookService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getBooks(getBooks: GetBooks): Observable<Page<Book>> {
    const url = `${this.apiUrl}/GetBooks`;
    return this.httpClient.post<Page<Book>>(url, getBooks);
  }

  getBook(getBook: GetBook): Observable<Book> {
    const url = `${this.apiUrl}/GetBook`;
    return this.httpClient.post<Book>(url, getBook);
  }

  exportBooks(exportBooks: ExportBooks): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportBooks`;
    return this.httpClient.post(url, exportBooks, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updateBook(updateBook: UpdateBook): Observable<{}> {
    const url = `${this.apiUrl}/UpdateBook`;
    return this.httpClient.post(url, updateBook);
  }

  createBook(createBook: CreateBook): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateBook`;
    return this.httpClient.post<CreatedEntity<number>>(url, createBook);
  }

  deleteBook(deleteBook: DeleteBook): Observable<{}> {
    const url = `${this.apiUrl}/DeleteBook`;
    return this.httpClient.post(url, deleteBook);
  }
}
