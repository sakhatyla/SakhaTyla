import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { Role } from './role.model';
import { GetRoles, GetRole, ExportRoles,
    UpdateRole, CreateRole, DeleteRole } from './role-request.model';

@Injectable({ providedIn: 'root' })
export class RoleService {
    private apiUrl = this.configService.config.apiBaseUrl + '/api';

    constructor(private httpClient: HttpClient, private configService: ConfigService) { }

    getRoles(getRoles: GetRoles): Observable<Page<Role>> {
        const url = `${this.apiUrl}/GetRoles`;
        return this.httpClient.post<Page<Role>>(url, getRoles);
    }

    getRole(getRole: GetRole): Observable<Role> {
        const url = `${this.apiUrl}/GetRole`;
        return this.httpClient.post<Role>(url, getRole);
    }

    exportRoles(exportRoles: ExportRoles): Observable<FileResult> {
        const url = `${this.apiUrl}/ExportRoles`;
        return this.httpClient.post(url, exportRoles, {
            responseType: 'blob' as 'json',
            observe: 'response',
        }).pipe(map((response => new FileResult(response))));
    }

    updateRole(updateRole: UpdateRole): Observable<{}> {
        const url = `${this.apiUrl}/UpdateRole`;
        return this.httpClient.post(url, updateRole);
    }

    createRole(createRole: CreateRole): Observable<CreatedEntity<number>> {
        const url = `${this.apiUrl}/CreateRole`;
        return this.httpClient.post<CreatedEntity<number>>(url, createRole);
    }

    deleteRole(deleteRole: DeleteRole): Observable<{}> {
        const url = `${this.apiUrl}/DeleteRole`;
        return this.httpClient.post(url, deleteRole);
    }
}
