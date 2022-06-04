import { BookLabelFilter } from './book-label-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetBookLabels {
  pageIndex?: number;
  pageSize?: number;
  filter?: BookLabelFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetBookLabel {
  id: number;
}

export class ExportBookLabels {
  filter?: BookLabelFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateBookLabel {
  id: number;
  name: string;
  pageId: number;
}

export class CreateBookLabel {
  bookId: number;
  name: string;
  pageId: number;
}

export class DeleteBookLabel {
  id: number;
}
