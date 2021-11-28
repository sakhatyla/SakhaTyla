import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

import { Role } from './role.model';
import { RoleService } from './role.service';

@Component({
  selector: 'app-roles-show',
  templateUrl: './roles-show.component.html'
})

export class RolesShowComponent implements OnInit, OnChanges {
  @Input()
  value: number[];

  roles: Role[] = [];
  userRoles: Role[] = [];

  constructor(private roleService: RoleService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.value) {
      this.setUserRoles();
    }
  }

  ngOnInit(): void {
    this.roleService.getRoles({})
      .subscribe(roles => {
        this.roles = roles.pageItems;
        this.setUserRoles();
      });
  }

  private setUserRoles() {
    this.userRoles = this.roles.filter(r => this.value && this.value.indexOf(r.id) !== -1);
  }
}
