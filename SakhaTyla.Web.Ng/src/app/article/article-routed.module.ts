import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ArticlesComponent } from './articles.component';
import { ArticleViewComponent } from './article-view.component';
import { ArticleModule } from '../article/article.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: ArticlesComponent },
      { path: ':id', component: ArticleViewComponent }
    ]),
    ArticleModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class ArticleRoutedModule {

}
