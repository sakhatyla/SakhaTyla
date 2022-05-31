import { EntityFilter } from '../core/models/entity-filter.model';
import { WidgetType } from '../widget-type/widget-type.model';

export class WidgetFilter extends EntityFilter {
  name?: string;
  code?: string;
  body?: string;
  type?: WidgetType;
}
