import { PageSettings } from '../core/page.model';
import { WorkerInfoFilter } from './worker-info-filter.model';

export class WorkerInfo {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  name: string;
  className: string;
}

export class WorkerInfoListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new WorkerInfoFilter();
}
