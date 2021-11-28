import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-time-view',
  templateUrl: './time.view.component.html'
})
export class TimeViewComponent {
  @Input()
  value: string;

  get valueWithDate(): string {
    if (this.value) {
      return `0000-00-00T${this.value}`;
    }
    return null;
  }
}
