import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { BookLabelFilter } from './book-label-filter.model';

export class BookLabel {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  bookId: number;
  name: string;
  pageId: number;

  constructor(bookId: number) {
    this.bookId = bookId;
  }
}

export class BookLabelListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new BookLabelFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
