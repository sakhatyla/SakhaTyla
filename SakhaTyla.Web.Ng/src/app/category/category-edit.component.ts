import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { Category } from '../category-core/category.model';
import { CategoryService } from '../category-core/category.service';

class DialogData {
    id: number;
}

@Component({
    selector: 'app-category-edit',
    templateUrl: './category-edit.component.html',
    styleUrls: ['./category-edit.component.scss']
})
export class CategoryEditComponent implements OnInit {
    id: number;
    categoryForm = this.fb.group({
        id: [],
        name: []
    });
    category: Category;
    error: Error;

    constructor(public dialogRef: MatDialogRef<CategoryEditComponent>,
                @Inject(MAT_DIALOG_DATA) public data: DialogData,
                private categoryService: CategoryService,
                private fb: FormBuilder,
                private noticeHelper: NoticeHelper) {
        this.id = data.id;
    }

    static show(dialog: MatDialog, id: number): Observable<any> {
        const dialogRef = dialog.open(CategoryEditComponent, {
            width: '600px',
            data: { id: id }
        });
        return dialogRef.afterClosed()
            .pipe(filter(res => res === true));
    }

    ngOnInit(): void {
        this.getCategory();
    }

    private getCategory() {
        const getCategory$ = !this.id ?
            of(new Category()) :
            this.categoryService.getCategory({ id: this.id });
        getCategory$.subscribe(category => {
            this.category = category;
            this.categoryForm.patchValue(this.category);
        });
    }

    onSave(): void {
        this.saveCategory();
    }

    private saveCategory() {
        const saveCategory$ = this.id ?
            this.categoryService.updateCategory(this.categoryForm.value) :
            this.categoryService.createCategory(this.categoryForm.value);
        saveCategory$.subscribe(() => this.dialogRef.close(true),
            error => this.onError(error));
    }

    onError(error: Error) {
        this.error = error;
        if (error) {
            this.noticeHelper.showError(error);
            Error.setFormErrors(this.categoryForm, error);
        }
    }
}
