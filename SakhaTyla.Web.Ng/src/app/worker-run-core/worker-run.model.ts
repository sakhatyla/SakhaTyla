import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { WorkerRunFilter } from './worker-run-filter.model';
import { WorkerRunStatus } from '../worker-run-status/worker-run-status.model';

export class WorkerRun {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  workerInfoId: number;
  status: WorkerRunStatus;
  startDateTime: Date;
  endDateTime: Date;
  data: string;
  result: string;
  resultData: string;
}

export class WorkerRunListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new WorkerRunFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;
}
