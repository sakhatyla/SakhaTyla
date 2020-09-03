import { Component, Input } from '@angular/core';

import { Category } from './category.model';
import { CategoryService } from './category.service';

@Component({
    selector: 'app-category-show',
    templateUrl: './category-show.component.html'
})

export class CategoryShowComponent {
    @Input()
    value: Category;

    @Input()
    set entityId(val: number) {
        this.categoryService.getCategory({ id: val })
            .subscribe(child => this.value = child);
    }

    constructor(private categoryService: CategoryService) {}
}
