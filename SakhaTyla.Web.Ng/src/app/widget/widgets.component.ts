import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { WidgetListState } from '../widget-core/widget.model';

@Component({
  selector: 'app-widgets',
  templateUrl: './widgets.component.html',
  styleUrls: ['./widgets.component.scss']
})
export class WidgetsComponent {

  state: WidgetListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('widgetListState', new WidgetListState());
  }

}
