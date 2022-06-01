import { BookFilter } from './book-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetBooks {
  pageIndex?: number;
  pageSize?: number;
  filter?: BookFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetBook {
  id: number;
}

export class ExportBooks {
  filter?: BookFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateBook {
  id: number;
  name: string;
  synonym: string;
  hidden: boolean;
  cover: string;
}

export class CreateBook {
  name: string;
  synonym: string;
  hidden: boolean;
  cover: string;
}

export class DeleteBook {
  id: number;
}
