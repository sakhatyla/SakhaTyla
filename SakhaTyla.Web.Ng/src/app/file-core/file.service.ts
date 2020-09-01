import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { File } from './file.model';
import { GetFiles, GetFile, ExportFiles, DownloadFile,
    UpdateFile, CreateFile, DeleteFile } from './file-request.model';

@Injectable({ providedIn: 'root' })
export class FileService {
    private apiUrl = this.configService.config.apiBaseUrl + '/api';

    constructor(private httpClient: HttpClient, private configService: ConfigService) { }

    getFiles(getFiles: GetFiles): Observable<Page<File>> {
        const url = `${this.apiUrl}/GetFiles`;
        return this.httpClient.post<Page<File>>(url, getFiles);
    }

    getFile(getFile: GetFile): Observable<File> {
        const url = `${this.apiUrl}/GetFile`;
        return this.httpClient.post<File>(url, getFile);
    }

    downloadFile(downloadFile: DownloadFile): Observable<FileResult> {
        const url = `${this.apiUrl}/DownloadFile`;
        return this.httpClient.post(url, downloadFile, {
            responseType: 'blob' as 'json',
            observe: 'response',
        }).pipe(map((response => new FileResult(response))));
    }

    exportFiles(exportFiles: ExportFiles): Observable<FileResult> {
        const url = `${this.apiUrl}/ExportFiles`;
        return this.httpClient.post(url, exportFiles, {
            responseType: 'blob' as 'json',
            observe: 'response',
        }).pipe(map((response => new FileResult(response))));
    }

    updateFile(updateFile: UpdateFile): Observable<{}> {
        const url = `${this.apiUrl}/UpdateFile`;
        const formData = new FormData();
        formData.append('id', `${updateFile.id}`);
        formData.append('file', updateFile.file);
        return this.httpClient.post(url, formData);
    }

    createFile(createFiles: CreateFile): Observable<CreatedEntity<number>> {
        const url = `${this.apiUrl}/CreateFile`;
        const formData = new FormData();
        formData.append('groupId', `${createFiles.groupId}`);
        formData.append('file', createFiles.file);
        return this.httpClient.post<CreatedEntity<number>>(url, formData);
    }

    deleteFile(deleteFile: DeleteFile): Observable<{}> {
        const url = `${this.apiUrl}/DeleteFile`;
        return this.httpClient.post(url, deleteFile);
    }
}
