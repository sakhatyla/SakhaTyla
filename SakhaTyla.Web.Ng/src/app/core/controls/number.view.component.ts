import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-number-view',
  templateUrl: './number.view.component.html'
})
export class NumberViewComponent {
  @Input()
  value: number;
}
