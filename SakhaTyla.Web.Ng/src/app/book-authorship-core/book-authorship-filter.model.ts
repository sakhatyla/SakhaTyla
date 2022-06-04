import { EntityFilter } from '../core/models/entity-filter.model';

export class BookAuthorshipFilter extends EntityFilter {
  bookId?: number;
  authorId?: number;
  weightFrom?: number;
  weightTo?: number;
}
