import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { EntityChange } from './entity-change.model';
import { GetEntityChanges } from './entity-change-request.model';

@Injectable({ providedIn: 'root' })
export class EntityChangeService {
    private apiUrl = this.configService.config.apiBaseUrl + '/api';

    constructor(private httpClient: HttpClient, private configService: ConfigService) { }

    getEntityChanges(getEntityChanges: GetEntityChanges): Observable<Page<EntityChange>> {
        const url = `${this.apiUrl}/GetEntityChanges`;
        return this.httpClient.post<Page<EntityChange>>(url, getEntityChanges);
    }
}
