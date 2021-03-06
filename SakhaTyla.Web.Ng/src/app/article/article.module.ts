﻿import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { ArticleCoreModule } from '../article-core/article-core.module';
import { LanguageCoreModule } from '../language-core/language-core.module';
import { CategoryCoreModule } from '../category-core/category-core.module';
import { EntityChangeModule } from '../entity-change/entity-change.module';

import { ArticleListComponent } from './article-list.component';
import { ArticleEditComponent } from './article-edit.component';
import { ArticleViewComponent } from './article-view.component';
import { ArticlesComponent } from './articles.component';
import { ArticleChangesComponent } from './article-changes.component';

@NgModule({
    declarations: [
        ArticleListComponent,
        ArticleEditComponent,
        ArticleViewComponent,
        ArticlesComponent,
        ArticleChangesComponent,
    ],
    imports: [
        RouterModule,
        CoreModule,
        TranslocoRootModule,
        LanguageCoreModule,
        CategoryCoreModule,
        ArticleCoreModule,
        EntityChangeModule,
    ],
    exports: [
        ArticleListComponent,
    ],
    providers: [
    ],
    entryComponents: [
        ArticleEditComponent
    ]
})
export class ArticleModule {

}
