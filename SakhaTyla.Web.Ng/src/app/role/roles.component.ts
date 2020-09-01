import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { RoleListState } from '../role-core/role.model';

@Component({
    selector: 'app-roles',
    templateUrl: './roles.component.html',
    styleUrls: ['./roles.component.scss']
})
export class RolesComponent {

    state: RoleListState;

    constructor(private storeService: StoreService) {
        this.state = this.storeService.get('roleListState', new RoleListState());
    }

}
