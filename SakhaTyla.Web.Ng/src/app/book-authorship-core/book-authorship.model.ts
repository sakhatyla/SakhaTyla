import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { BookAuthorshipFilter } from './book-authorship-filter.model';

export class BookAuthorship {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  bookId: number;
  authorId: number;
  weight: number;
}

export class BookAuthorshipListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new BookAuthorshipFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
