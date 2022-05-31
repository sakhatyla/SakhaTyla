import { WidgetFilter } from './widget-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';
import { WidgetType } from '../widget-type/widget-type.model';

export class GetWidgets {
  pageIndex?: number;
  pageSize?: number;
  filter?: WidgetFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetWidget {
  id: number;
}

export class ExportWidgets {
  filter?: WidgetFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateWidget {
  id: number;
  name: string;
  code: string;
  body: string;
  type: WidgetType;
}

export class CreateWidget {
  name: string;
  code: string;
  body: string;
  type: WidgetType;
}

export class DeleteWidget {
  id: number;
}
