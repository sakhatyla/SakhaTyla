import { Component, Input } from '@angular/core';

import { ChangeAction, ChangeActionDisplay } from './change-action.model';

@Component({
  selector: 'app-change-action-view',
  templateUrl: './change-action-view.component.html'
})

export class ChangeActionViewComponent {
  constructor() { }

  ChangeActionDisplay = ChangeActionDisplay;

  @Input()
  value: ChangeAction;
}
