import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { ArticleTagsSelectComponent } from './article-tags-select.component';
import { ArticleTagsShowComponent } from './article-tags-show.component';

@NgModule({
  declarations: [
    ArticleTagsSelectComponent,
    ArticleTagsShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
  ],
  exports: [
    ArticleTagsSelectComponent,
    ArticleTagsShowComponent
  ]
})
export class ArticleTagCoreModule {

}
