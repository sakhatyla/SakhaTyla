import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { MenuItemFilter } from './menu-item-filter.model';

export class MenuItem {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  treePath: string;
  treeOrder: string;
  menuId: number;
  name: string;
  url: string;
  weight: number;
  parentId: number;
}

export class MenuItemListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new MenuItemFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
