import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { TagFilter } from './tag-filter.model';

export class Tag {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  name: string;
}

export class TagListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new TagFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
