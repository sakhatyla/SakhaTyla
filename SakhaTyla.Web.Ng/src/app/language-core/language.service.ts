import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { Language } from './language.model';
import { GetLanguages, GetLanguage, ExportLanguages,
    UpdateLanguage, CreateLanguage, DeleteLanguage } from './language-request.model';

@Injectable({ providedIn: 'root' })
export class LanguageService {
    private apiUrl = this.configService.config.apiBaseUrl + '/api';

    constructor(private httpClient: HttpClient, private configService: ConfigService) { }

    getLanguages(getLanguages: GetLanguages): Observable<Page<Language>> {
        const url = `${this.apiUrl}/GetLanguages`;
        return this.httpClient.post<Page<Language>>(url, getLanguages);
    }

    getLanguage(getLanguage: GetLanguage): Observable<Language> {
        const url = `${this.apiUrl}/GetLanguage`;
        return this.httpClient.post<Language>(url, getLanguage);
    }

    exportLanguages(exportLanguages: ExportLanguages): Observable<FileResult> {
        const url = `${this.apiUrl}/ExportLanguages`;
        return this.httpClient.post(url, exportLanguages, {
            responseType: 'blob' as 'json',
            observe: 'response',
        }).pipe(map((response => new FileResult(response))));
    }

    updateLanguage(updateLanguage: UpdateLanguage): Observable<{}> {
        const url = `${this.apiUrl}/UpdateLanguage`;
        return this.httpClient.post(url, updateLanguage);
    }

    createLanguage(createLanguage: CreateLanguage): Observable<CreatedEntity<number>> {
        const url = `${this.apiUrl}/CreateLanguage`;
        return this.httpClient.post<CreatedEntity<number>>(url, createLanguage);
    }

    deleteLanguage(deleteLanguage: DeleteLanguage): Observable<{}> {
        const url = `${this.apiUrl}/DeleteLanguage`;
        return this.httpClient.post(url, deleteLanguage);
    }
}
