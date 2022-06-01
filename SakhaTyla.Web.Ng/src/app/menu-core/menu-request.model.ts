import { MenuFilter } from './menu-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetMenus {
  pageIndex?: number;
  pageSize?: number;
  filter?: MenuFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetMenu {
  id: number;
}

export class ExportMenus {
  filter?: MenuFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateMenu {
  id: number;
  name: string;
  code: string;
}

export class CreateMenu {
  name: string;
  code: string;
}

export class DeleteMenu {
  id: number;
}
