import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { ArticleFilter } from './article-filter.model';

export class Article {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  title: string;
  text: string;
  textSource: string;
  fromLanguageId: number;
  toLanguageId: number;
  fuzzy: boolean;
  categoryId: number;
}

export class ArticleListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new ArticleFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
