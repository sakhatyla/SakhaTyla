import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { Route } from '../route-core/route.model';
import { PageFilter } from './page-filter.model';
import { PageType } from '../page-type/page-type.model';

export class Page {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  treePath: string;
  treeOrder: string;
  type: PageType;
  name: string;
  shortName: string;
  parentId: number;
  header: string;
  body: string;
  metaTitle: string;
  metaKeywords: string;
  metaDescription: string;
  imageId: number;
  preview: string;
  route: Route;
  commentContainerId: number;
}

export class PageListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new PageFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
