import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { Category } from '../category-core/category.model';
import { CategoryService } from '../category-core/category.service';
import { CategoryEditComponent } from './category-edit.component';

@Component({
    selector: 'app-category-view',
    templateUrl: './category-view.component.html',
    styleUrls: ['./category-view.component.scss']
})
export class CategoryViewComponent implements OnInit {
    id: number;
    category: Category;

    constructor(private dialog: MatDialog,
                private categoryService: CategoryService,
                private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.route.params.forEach((params: Params) => {
            this.id = ConvertStringTo.number(params.id);
            this.getCategory();
        });
    }

    private getCategory() {
        this.categoryService.getCategory({ id: this.id })
            .subscribe(category => this.category = category);
    }

    onEdit() {
        CategoryEditComponent.show(this.dialog, this.id).subscribe(() => {
            this.getCategory();
        });
    }

    onBack(): void {
        window.history.back();
    }
}
