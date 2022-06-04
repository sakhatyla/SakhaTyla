import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { BookAuthorFilter } from './book-author-filter.model';

export class BookAuthor {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  lastName: string;
  firstName: string;
  middleName: string;
  nickName: string;
}

export class BookAuthorListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new BookAuthorFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
