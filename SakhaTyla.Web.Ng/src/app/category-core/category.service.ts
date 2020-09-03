import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { Category } from './category.model';
import { GetCategories, GetCategory, ExportCategories,
    UpdateCategory, CreateCategory, DeleteCategory } from './category-request.model';

@Injectable({ providedIn: 'root' })
export class CategoryService {
    private apiUrl = this.configService.config.apiBaseUrl + '/api';

    constructor(private httpClient: HttpClient, private configService: ConfigService) { }

    getCategories(getCategories: GetCategories): Observable<Page<Category>> {
        const url = `${this.apiUrl}/GetCategories`;
        return this.httpClient.post<Page<Category>>(url, getCategories);
    }

    getCategory(getCategory: GetCategory): Observable<Category> {
        const url = `${this.apiUrl}/GetCategory`;
        return this.httpClient.post<Category>(url, getCategory);
    }

    exportCategories(exportCategories: ExportCategories): Observable<FileResult> {
        const url = `${this.apiUrl}/ExportCategories`;
        return this.httpClient.post(url, exportCategories, {
            responseType: 'blob' as 'json',
            observe: 'response',
        }).pipe(map((response => new FileResult(response))));
    }

    updateCategory(updateCategory: UpdateCategory): Observable<{}> {
        const url = `${this.apiUrl}/UpdateCategory`;
        return this.httpClient.post(url, updateCategory);
    }

    createCategory(createCategory: CreateCategory): Observable<CreatedEntity<number>> {
        const url = `${this.apiUrl}/CreateCategory`;
        return this.httpClient.post<CreatedEntity<number>>(url, createCategory);
    }

    deleteCategory(deleteCategory: DeleteCategory): Observable<{}> {
        const url = `${this.apiUrl}/DeleteCategory`;
        return this.httpClient.post(url, deleteCategory);
    }
}
