import { CategoryFilter } from './category-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetCategories {
  pageIndex?: number;
  pageSize?: number;
  filter?: CategoryFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetCategory {
  id: number;
}

export class ExportCategories {
  filter?: CategoryFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateCategory {
  id: number;
  name: string;
}

export class CreateCategory {
  name: string;
}

export class DeleteCategory {
  id: number;
}
