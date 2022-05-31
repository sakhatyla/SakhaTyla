import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { WidgetFilter } from './widget-filter.model';
import { WidgetType } from '../widget-type/widget-type.model';

export class Widget {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  name: string;
  code: string;
  body: string;
  type: WidgetType;
}

export class WidgetListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new WidgetFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
