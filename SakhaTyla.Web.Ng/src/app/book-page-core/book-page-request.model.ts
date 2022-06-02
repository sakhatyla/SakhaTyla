import { BookPageFilter } from './book-page-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetBookPages {
  pageIndex?: number;
  pageSize?: number;
  filter?: BookPageFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetBookPage {
  id: number;
}

export class ExportBookPages {
  filter?: BookPageFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateBookPage {
  id: number;
  fileName: string;
  number: number;
}

export class CreateBookPage {
  bookId: number;
  fileName: string;
  number: number;
}

export class DeleteBookPage {
  id: number;
}
