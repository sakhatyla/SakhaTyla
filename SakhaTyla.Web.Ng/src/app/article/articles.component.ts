import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { ArticleListState } from '../article-core/article.model';

@Component({
  selector: 'app-articles',
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent {

  state: ArticleListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('articleListState', new ArticleListState());
  }

}
