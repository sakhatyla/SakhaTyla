import { EntityFilter } from '../core/models/entity-filter.model';

export class BookFilter extends EntityFilter {
  name?: string;
  synonym?: string;
  hidden?: boolean;
  cover?: string;
}
