﻿import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Menu } from './menu.model';
import { AuthorizeService } from '../../api-authorization/authorize.service';

@Injectable()
export class MenuService {
    private menu: Menu;

    constructor(private authorize: AuthorizeService) {
        this.menu = new Menu();
// ADD MENU ITEMS HERE
        this.menu.items.push({ route: '/file', name: 'Files', icon: 'insert_drive_file', roles: ['Administrator'] });
        this.menu.items.push({ route: '/file-group', name: 'File Groups', icon: 'folder_open', roles: ['Administrator'] });
        this.menu.items.push({ route: '/user', name: 'Users', icon: 'person', roles: ['Administrator'] });
        this.menu.items.push({ route: '/role', name: 'Roles', icon: 'lock', roles: ['Administrator'] });
    }

    getMenu(): Observable<Menu> {
        return this.authorize.getRoles().pipe(map(roles => {
            const menu: Menu = {
                items: this.menu.items.filter(item => {
                    return item.roles.some(role => roles.includes(role));
                })
            };
            return menu;
        }));
    }
}
