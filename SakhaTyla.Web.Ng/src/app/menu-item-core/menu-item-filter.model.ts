import { EntityFilter } from '../core/models/entity-filter.model';

export class MenuItemFilter extends EntityFilter {
  menuId?: number;
  name?: string;
  url?: string;
  weightFrom?: number;
  weightTo?: number;
  parentId?: number;
}
