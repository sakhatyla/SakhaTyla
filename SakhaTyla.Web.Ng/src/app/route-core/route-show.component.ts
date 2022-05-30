import { Component, Input } from '@angular/core';

import { Route } from './route.model';

@Component({
  selector: 'app-route-show',
  templateUrl: './route-show.component.html'
})

export class RouteShowComponent {
  @Input()
  value: Route;
}
