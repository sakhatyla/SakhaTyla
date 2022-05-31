import { Component, Input } from '@angular/core';

import { Widget } from './widget.model';

@Component({
  selector: 'app-widget-show',
  templateUrl: './widget-show.component.html'
})

export class WidgetShowComponent {
  @Input()
  value: Widget;
}
