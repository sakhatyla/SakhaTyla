import { TagFilter } from './tag-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetTags {
  pageIndex?: number;
  pageSize?: number;
  filter?: TagFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetTag {
  id: number;
}

export class ExportTags {
  filter?: TagFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateTag {
  id: number;
  name: string;
}

export class CreateTag {
  name: string;
}

export class DeleteTag {
  id: number;
}
