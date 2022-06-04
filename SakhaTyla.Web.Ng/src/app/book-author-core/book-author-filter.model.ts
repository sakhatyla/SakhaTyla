import { EntityFilter } from '../core/models/entity-filter.model';

export class BookAuthorFilter extends EntityFilter {
  lastName?: string;
  firstName?: string;
  middleName?: string;
  nickName?: string;
}
