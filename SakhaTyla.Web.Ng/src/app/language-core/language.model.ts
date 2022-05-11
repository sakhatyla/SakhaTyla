import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { LanguageFilter } from './language-filter.model';

export class Language {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  name: string;
  code: string;
}

export class LanguageListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new LanguageFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
