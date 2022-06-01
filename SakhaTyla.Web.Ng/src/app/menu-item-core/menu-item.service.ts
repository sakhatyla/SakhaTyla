import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { MenuItem } from './menu-item.model';
import { GetMenuItems, GetMenuItem, ExportMenuItems,
  UpdateMenuItem, CreateMenuItem, DeleteMenuItem, UpdateMenuItemWeight } from './menu-item-request.model';

@Injectable({ providedIn: 'root' })
export class MenuItemService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getMenuItems(getMenuItems: GetMenuItems): Observable<Page<MenuItem>> {
    const url = `${this.apiUrl}/GetMenuItems`;
    return this.httpClient.post<Page<MenuItem>>(url, getMenuItems);
  }

  getMenuItem(getMenuItem: GetMenuItem): Observable<MenuItem> {
    const url = `${this.apiUrl}/GetMenuItem`;
    return this.httpClient.post<MenuItem>(url, getMenuItem);
  }

  exportMenuItems(exportMenuItems: ExportMenuItems): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportMenuItems`;
    return this.httpClient.post(url, exportMenuItems, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updateMenuItem(updateMenuItem: UpdateMenuItem): Observable<{}> {
    const url = `${this.apiUrl}/UpdateMenuItem`;
    return this.httpClient.post(url, updateMenuItem);
  }

  updateMenuItemWeight(updateMenuItemWeight: UpdateMenuItemWeight): Observable<{}> {
    const url = `${this.apiUrl}/UpdateMenuItemWeight`;
    return this.httpClient.post(url, updateMenuItemWeight);
  }

  createMenuItem(createMenuItem: CreateMenuItem): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateMenuItem`;
    return this.httpClient.post<CreatedEntity<number>>(url, createMenuItem);
  }

  deleteMenuItem(deleteMenuItem: DeleteMenuItem): Observable<{}> {
    const url = `${this.apiUrl}/DeleteMenuItem`;
    return this.httpClient.post(url, deleteMenuItem);
  }
}
