import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigService } from '../config/config.service';
import { CreatedEntity } from '../core/models/created-entity.model';
import { Page } from '../core/page.model';
import { FileResult } from '../core/file-result.model';

import { Menu } from './menu.model';
import { GetMenus, GetMenu, ExportMenus,
  UpdateMenu, CreateMenu, DeleteMenu } from './menu-request.model';

@Injectable({ providedIn: 'root' })
export class MenuService {
  private apiUrl = this.configService.config.apiBaseUrl + '/api';

  constructor(private httpClient: HttpClient, private configService: ConfigService) { }

  getMenus(getMenus: GetMenus): Observable<Page<Menu>> {
    const url = `${this.apiUrl}/GetMenus`;
    return this.httpClient.post<Page<Menu>>(url, getMenus);
  }

  getMenu(getMenu: GetMenu): Observable<Menu> {
    const url = `${this.apiUrl}/GetMenu`;
    return this.httpClient.post<Menu>(url, getMenu);
  }

  exportMenus(exportMenus: ExportMenus): Observable<FileResult> {
    const url = `${this.apiUrl}/ExportMenus`;
    return this.httpClient.post(url, exportMenus, {
      responseType: 'blob' as 'json',
      observe: 'response',
    }).pipe(map((response => new FileResult(response))));
  }

  updateMenu(updateMenu: UpdateMenu): Observable<{}> {
    const url = `${this.apiUrl}/UpdateMenu`;
    return this.httpClient.post(url, updateMenu);
  }

  createMenu(createMenu: CreateMenu): Observable<CreatedEntity<number>> {
    const url = `${this.apiUrl}/CreateMenu`;
    return this.httpClient.post<CreatedEntity<number>>(url, createMenu);
  }

  deleteMenu(deleteMenu: DeleteMenu): Observable<{}> {
    const url = `${this.apiUrl}/DeleteMenu`;
    return this.httpClient.post(url, deleteMenu);
  }
}
