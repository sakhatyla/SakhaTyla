import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { FileGroup } from './file-group.model';
import { GetFileGroups, GetFileGroup, ExportFileGroups,
    UpdateFileGroup, CreateFileGroup, DeleteFileGroup } from './file-group-request.model';

@Injectable({ providedIn: 'root' })
export class FileGroupService {
    private apiUrl = this.configService.config.apiBaseUrl + '/api';

    constructor(private httpClient: HttpClient, private configService: ConfigService) { }

    getFileGroups(getFileGroups: GetFileGroups): Observable<Page<FileGroup>> {
        const url = `${this.apiUrl}/GetFileGroups`;
        return this.httpClient.post<Page<FileGroup>>(url, getFileGroups);
    }

    getFileGroup(getFileGroup: GetFileGroup): Observable<FileGroup> {
        const url = `${this.apiUrl}/GetFileGroup`;
        return this.httpClient.post<FileGroup>(url, getFileGroup);
    }

    exportFileGroups(exportFileGroups: ExportFileGroups): Observable<FileResult> {
        const url = `${this.apiUrl}/ExportFileGroups`;
        return this.httpClient.post(url, exportFileGroups, {
            responseType: 'blob' as 'json',
            observe: 'response',
        }).pipe(map((response => new FileResult(response))));
    }

    updateFileGroup(updateFileGroup: UpdateFileGroup): Observable<{}> {
        const url = `${this.apiUrl}/UpdateFileGroup`;
        return this.httpClient.post(url, updateFileGroup);
    }

    createFileGroup(createFileGroup: CreateFileGroup): Observable<CreatedEntity<number>> {
        const url = `${this.apiUrl}/CreateFileGroup`;
        return this.httpClient.post<CreatedEntity<number>>(url, createFileGroup);
    }

    deleteFileGroup(deleteFileGroup: DeleteFileGroup): Observable<{}> {
        const url = `${this.apiUrl}/DeleteFileGroup`;
        return this.httpClient.post(url, deleteFileGroup);
    }
}
