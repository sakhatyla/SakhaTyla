import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { CategoryCoreModule } from '../category-core/category-core.module';

import { CategoryListComponent } from './category-list.component';
import { CategoryEditComponent } from './category-edit.component';
import { CategoryViewComponent } from './category-view.component';
import { CategoriesComponent } from './categories.component';

@NgModule({
  declarations: [
    CategoryListComponent,
    CategoryEditComponent,
    CategoryViewComponent,
    CategoriesComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    CategoryCoreModule
  ],
  exports: [
    CategoryListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    CategoryEditComponent
  ]
})
export class CategoryModule {

}
