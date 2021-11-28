import { Component, Input } from '@angular/core';

import { Role } from './role.model';

@Component({
  selector: 'app-role-show',
  templateUrl: './role-show.component.html'
})

export class RoleShowComponent {
  @Input()
  value: Role;
}
