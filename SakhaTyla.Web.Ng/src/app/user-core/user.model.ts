import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { UserFilter } from './user-filter.model';

export class User {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  userName: string;
  email: string;
  emailConfirmed: boolean;
  roleIds: number[];
  firstName: string;
  lastName: string;
}

export class UserListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new UserFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
