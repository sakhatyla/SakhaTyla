import { BookAuthorFilter } from './book-author-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetBookAuthors {
  pageIndex?: number;
  pageSize?: number;
  filter?: BookAuthorFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetBookAuthor {
  id: number;
}

export class ExportBookAuthors {
  filter?: BookAuthorFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateBookAuthor {
  id: number;
  lastName: string;
  firstName: string;
  middleName: string;
  nickName: string;
}

export class CreateBookAuthor {
  lastName: string;
  firstName: string;
  middleName: string;
  nickName: string;
}

export class DeleteBookAuthor {
  id: number;
}
