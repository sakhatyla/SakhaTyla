import { Component, Input } from '@angular/core';

import { Role } from './role.model';

@Component({
  selector: 'app-roles-show',
  templateUrl: './roles-show.component.html'
})

export class RolesShowComponent {
  @Input()
  value: Role[];

  constructor() { }
}
