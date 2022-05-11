import { WorkerScheduleTaskFilter } from './worker-schedule-task-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetWorkerScheduleTasks {
  pageIndex?: number;
  pageSize?: number;
  filter?: WorkerScheduleTaskFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetWorkerScheduleTask {
  id: number;
}

export class ExportWorkerScheduleTasks {
  filter?: WorkerScheduleTaskFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateWorkerScheduleTask {
  id: number;
  seconds: string;
  minutes: string;
  hours: string;
  dayOfMonth: string;
  month: string;
  dayOfWeek: string;
  year: string;
}

export class CreateWorkerScheduleTask {
  workerInfoId: number;
  seconds: string;
  minutes: string;
  hours: string;
  dayOfMonth: string;
  month: string;
  dayOfWeek: string;
  year: string;
}

export class DeleteWorkerScheduleTask {
  id: number;
}
