import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-date-view',
    templateUrl: './date.view.component.html'
})
export class DateViewComponent {
    @Input()
    value: Date;
}
