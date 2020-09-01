import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { User } from './user.model';
import { GetUsers, GetUser, ExportUsers,
    UpdateUser, CreateUser, DeleteUser } from './user-request.model';

@Injectable({ providedIn: 'root' })
export class UserService {
    private apiUrl = this.configService.config.apiBaseUrl + '/api';

    constructor(private httpClient: HttpClient, private configService: ConfigService) { }

    getUsers(getUsers: GetUsers): Observable<Page<User>> {
        const url = `${this.apiUrl}/GetUsers`;
        return this.httpClient.post<Page<User>>(url, getUsers);
    }

    getUser(getUser: GetUser): Observable<User> {
        const url = `${this.apiUrl}/GetUser`;
        return this.httpClient.post<User>(url, getUser);
    }

    exportUsers(exportUsers: ExportUsers): Observable<FileResult> {
        const url = `${this.apiUrl}/ExportUsers`;
        return this.httpClient.post(url, exportUsers, {
            responseType: 'blob' as 'json',
            observe: 'response',
        }).pipe(map((response => new FileResult(response))));
    }

    updateUser(updateUser: UpdateUser): Observable<{}> {
        const url = `${this.apiUrl}/UpdateUser`;
        if (!updateUser.password) {
            updateUser.password = null;
        }
        return this.httpClient.post(url, updateUser);
    }

    createUser(createUser: CreateUser): Observable<CreatedEntity<number>> {
        const url = `${this.apiUrl}/CreateUser`;
        return this.httpClient.post<CreatedEntity<number>>(url, createUser);
    }

    deleteUser(deleteUser: DeleteUser): Observable<{}> {
        const url = `${this.apiUrl}/DeleteUser`;
        return this.httpClient.post(url, deleteUser);
    }
}
