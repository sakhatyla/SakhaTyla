import { Component, Input } from '@angular/core';

import { Category } from './category.model';

@Component({
    selector: 'app-category-show',
    templateUrl: './category-show.component.html'
})

export class CategoryShowComponent {
    @Input()
    value: Category;
}
