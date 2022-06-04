import { EntityFilter } from '../core/models/entity-filter.model';

export class BookLabelFilter extends EntityFilter {
  bookId?: number;
  name?: string;
  pageId?: number;
}
