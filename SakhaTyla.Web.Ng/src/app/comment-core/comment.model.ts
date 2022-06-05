import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { Page } from '../page-core/page.model';
import { CommentFilter } from './comment-filter.model';

export class Comment {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  treePath: string;
  treeOrder: string;
  containerId: number;
  text: string;
  textSource: string;
  authorId: number;
  parentId: number;
}

export class CommentContainer {
  page: Page;
}

export class CommentListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new CommentFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
