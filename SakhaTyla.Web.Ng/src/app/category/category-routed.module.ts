import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CategoriesComponent } from './categories.component';
import { CategoryViewComponent } from './category-view.component';
import { CategoryModule } from '../category/category.module';

@NgModule({
    declarations: [
    ],
    imports: [
        RouterModule.forChild([
            { path: '', component: CategoriesComponent },
            { path: ':id', component: CategoryViewComponent }
        ]),
        CategoryModule,
    ],
    providers: [
    ],
    entryComponents: [
    ]
})
export class CategoryRoutedModule {

}
