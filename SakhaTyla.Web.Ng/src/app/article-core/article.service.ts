import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { Article } from './article.model';
import { GetArticles, GetArticle, ExportArticles,
  UpdateArticle, CreateArticle, DeleteArticle, ImportArticles } from './article-request.model';

@Injectable({ providedIn: 'root' })
export class ArticleService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getArticles(getArticles: GetArticles): Observable<Page<Article>> {
    const url = `${this.apiUrl}/GetArticles`;
    return this.httpClient.post<Page<Article>>(url, getArticles);
  }

  getArticle(getArticle: GetArticle): Observable<Article> {
    const url = `${this.apiUrl}/GetArticle`;
    return this.httpClient.post<Article>(url, getArticle);
  }

  exportArticles(exportArticles: ExportArticles): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportArticles`;
    return this.httpClient.post(url, exportArticles, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updateArticle(updateArticle: UpdateArticle): Observable<{}> {
    const url = `${this.apiUrl}/UpdateArticle`;
    return this.httpClient.post(url, updateArticle);
  }

  createArticle(createArticle: CreateArticle): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateArticle`;
    return this.httpClient.post<CreatedEntity<number>>(url, createArticle);
  }

  deleteArticle(deleteArticle: DeleteArticle): Observable<{}> {
    const url = `${this.apiUrl}/DeleteArticle`;
    return this.httpClient.post(url, deleteArticle);
  }

  importArticles(importArticles: ImportArticles): Observable<{}> {
    const url = `${this.apiUrl}/ImportArticles`;
    return this.httpClient.post(url, importArticles);
  }
}
