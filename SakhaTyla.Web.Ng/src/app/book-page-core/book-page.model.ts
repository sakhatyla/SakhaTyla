import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { BookPageFilter } from './book-page-filter.model';

export class BookPage {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  bookId: number;
  fileName: string;
  number: number;
}

export class BookPageListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new BookPageFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
