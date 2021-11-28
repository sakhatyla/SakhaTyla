import { WorkerRunFilter } from './worker-run-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';
import { WorkerRunStatus } from '../worker-run-status/worker-run-status.model';

export class GetWorkerRuns {
  pageIndex?: number;
  pageSize?: number;
  filter?: WorkerRunFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetWorkerRun {
  id: number;
}

export class ExportWorkerRuns {
  filter?: WorkerRunFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class CreateWorkerRun {
  workerInfoId: number;
  data: string;
}

export class DeleteWorkerRun {
  id: number;
}
