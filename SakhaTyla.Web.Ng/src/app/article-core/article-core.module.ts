import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { ArticleService } from './article.service';
import { ArticleSelectComponent } from './article-select.component';
import { ArticleShowComponent } from './article-show.component';

@NgModule({
  declarations: [
    ArticleSelectComponent,
    ArticleShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    ArticleService
  ],
  exports: [
    ArticleSelectComponent,
    ArticleShowComponent
  ]
})
export class ArticleCoreModule {

}
