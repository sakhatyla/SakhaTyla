import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { CategoryService } from './category.service';
import { CategorySelectComponent } from './category-select.component';
import { CategoryShowComponent } from './category-show.component';

@NgModule({
    declarations: [
        CategorySelectComponent,
        CategoryShowComponent
    ],
    imports: [
        CoreModule,
    ],
    providers: [
        CategoryService
    ],
    exports: [
        CategorySelectComponent,
        CategoryShowComponent
    ]
})
export class CategoryCoreModule {

}
