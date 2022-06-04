import { BookAuthorshipFilter } from './book-authorship-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetBookAuthorships {
  pageIndex?: number;
  pageSize?: number;
  filter?: BookAuthorshipFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetBookAuthorship {
  id: number;
}

export class ExportBookAuthorships {
  filter?: BookAuthorshipFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateBookAuthorship {
  id: number;
  authorId: number;
  weight: number;
}

export class CreateBookAuthorship {
  bookId: number;
  authorId: number;
  weight: number;
}

export class DeleteBookAuthorship {
  id: number;
}
