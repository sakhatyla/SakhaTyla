import { EntityFilter } from '../core/models/entity-filter.model';

export class UserFilter extends EntityFilter {
    userName?: string;
    email?: string;
    firstName?: string;
    lastName?: string;
}
