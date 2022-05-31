import { Component, Input } from '@angular/core';

import { WidgetType, WidgetTypeDisplay } from './widget-type.model';

@Component({
  selector: 'app-widget-type-view',
  templateUrl: './widget-type-view.component.html'
})

export class WidgetTypeViewComponent {
  constructor() { }

  WidgetTypeDisplay = WidgetTypeDisplay;

  @Input()
  value: WidgetType;
}
