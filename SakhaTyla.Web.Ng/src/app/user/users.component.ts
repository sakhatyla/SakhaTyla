import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { UserListState } from '../user-core/user.model';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent {

  state: UserListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('userListState', new UserListState());
  }

}
