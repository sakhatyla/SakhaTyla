import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { User } from '../user-core/user.model';
import { UserService } from '../user-core/user.service';
import { UserEditComponent } from './user-edit.component';

@Component({
  selector: 'app-user-view',
  templateUrl: './user-view.component.html',
  styleUrls: ['./user-view.component.scss']
})
export class UserViewComponent implements OnInit {
  id: number;
  user: User;

  constructor(private dialog: MatDialog,
              private userService: UserService,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getUser();
    });
  }

  private getUser() {
    this.userService.getUser({ id: this.id }).subscribe(user => {
      this.user = user;
    });
  }

  onEdit() {
    UserEditComponent.show(this.dialog, this.id).subscribe(() => {
      this.getUser();
    });
  }

  onBack() {
    window.history.back();
  }

}
