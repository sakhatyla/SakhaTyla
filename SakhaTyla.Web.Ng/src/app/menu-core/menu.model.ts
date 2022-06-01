import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { MenuFilter } from './menu-filter.model';

export class Menu {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  name: string;
  code: string;
}

export class MenuListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new MenuFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
