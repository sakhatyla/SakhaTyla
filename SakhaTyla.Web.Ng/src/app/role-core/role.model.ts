import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { RoleFilter } from './role-filter.model';

export class Role {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  name: string;
  isSelected: boolean;
  displayName: string;
}

export class RoleListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new RoleFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
