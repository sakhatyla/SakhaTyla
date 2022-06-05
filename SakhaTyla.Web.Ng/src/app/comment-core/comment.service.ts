import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { Comment, CommentContainer } from './comment.model';
import { GetComments, GetComment, ExportComments,
  UpdateComment, CreateComment, DeleteComment, GetCommentContainer } from './comment-request.model';

@Injectable({ providedIn: 'root' })
export class CommentService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getComments(getComments: GetComments): Observable<Page<Comment>> {
    const url = `${this.apiUrl}/GetComments`;
    return this.httpClient.post<Page<Comment>>(url, getComments);
  }

  getComment(getComment: GetComment): Observable<Comment> {
    const url = `${this.apiUrl}/GetComment`;
    return this.httpClient.post<Comment>(url, getComment);
  }

  getCommentContainer(getCommentContainer: GetCommentContainer): Observable<CommentContainer> {
    const url = `${this.apiUrl}/GetCommentContainer`;
    return this.httpClient.post<CommentContainer>(url, getCommentContainer);
  }

  exportComments(exportComments: ExportComments): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportComments`;
    return this.httpClient.post(url, exportComments, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updateComment(updateComment: UpdateComment): Observable<{}> {
    const url = `${this.apiUrl}/UpdateComment`;
    return this.httpClient.post(url, updateComment);
  }

  createComment(createComment: CreateComment): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateComment`;
    return this.httpClient.post<CreatedEntity<number>>(url, createComment);
  }

  deleteComment(deleteComment: DeleteComment): Observable<{}> {
    const url = `${this.apiUrl}/DeleteComment`;
    return this.httpClient.post(url, deleteComment);
  }
}
