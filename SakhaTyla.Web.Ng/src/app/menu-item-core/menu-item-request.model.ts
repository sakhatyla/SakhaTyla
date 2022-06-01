import { MenuItemFilter } from './menu-item-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetMenuItems {
  pageIndex?: number;
  pageSize?: number;
  filter?: MenuItemFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetMenuItem {
  id: number;
}

export class ExportMenuItems {
  filter?: MenuItemFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateMenuItem {
  id: number;
  menuId: number;
  name: string;
  url: string;
  parentId: number;
}

export class UpdateMenuItemWeight {
  menuId: number;
  parentId: number;
  ids: number[];
}


export class CreateMenuItem {
  menuId: number;
  name: string;
  url: string;
  parentId: number;
}

export class DeleteMenuItem {
  id: number;
}
