import { EntityFilter } from '../core/models/entity-filter.model';

export class BookPageFilter extends EntityFilter {
  bookId?: number;
  fileName?: string;
  numberFrom?: number;
  numberTo?: number;
}
