import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { BookFilter } from './book-filter.model';

export class Book {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  name: string;
  synonym: string;
  hidden: boolean;
  cover: string;
}

export class BookListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new BookFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
