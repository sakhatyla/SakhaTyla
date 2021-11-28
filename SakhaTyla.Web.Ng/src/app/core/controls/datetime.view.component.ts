import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-datetime-view',
  templateUrl: './datetime.view.component.html'
})
export class DateTimeViewComponent {
  @Input()
  value: Date;
}
