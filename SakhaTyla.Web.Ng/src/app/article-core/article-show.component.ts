import { Component, Input } from '@angular/core';

import { Article } from './article.model';

@Component({
  selector: 'app-article-show',
  templateUrl: './article-show.component.html'
})

export class ArticleShowComponent {
  @Input()
  value: Article;
}
