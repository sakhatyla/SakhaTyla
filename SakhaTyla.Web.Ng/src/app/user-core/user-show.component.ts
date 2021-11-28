import { Component, Input } from '@angular/core';

import { User } from './user.model';

@Component({
  selector: 'app-user-show',
  templateUrl: './user-show.component.html'
})

export class UserShowComponent {
  @Input()
  value: User;
}
