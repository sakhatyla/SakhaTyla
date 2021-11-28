import { WorkerInfoFilter } from './worker-info-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetWorkerInfos {
  pageIndex?: number;
  pageSize?: number;
  filter?: WorkerInfoFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetWorkerInfo {
  id: number;
}

export class ExportWorkerInfos {
  filter?: WorkerInfoFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateWorkerInfo {
  id: number;
  name: string;
  className: string;
}

export class CreateWorkerInfo {
  name: string;
  className: string;
}

export class DeleteWorkerInfo {
  id: number;
}
