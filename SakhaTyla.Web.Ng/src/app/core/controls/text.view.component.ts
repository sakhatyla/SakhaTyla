import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-text-view',
  templateUrl: './text.view.component.html'
})
export class TextViewComponent {
  @Input()
  value: string;
}
