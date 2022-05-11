import { OrderDirection } from '../core/models/order-direction.model';
import { PageSettings } from '../core/page.model';
import { WorkerScheduleTaskFilter } from './worker-schedule-task-filter.model';

export class WorkerScheduleTask {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  workerInfoId: number;
  seconds: string;
  minutes: string;
  hours: string;
  dayOfMonth: string;
  month: string;
  dayOfWeek: string;
  year: string;

  constructor(workerInfoId: number) {
    this.workerInfoId = workerInfoId;
  }
}

export class WorkerScheduleTaskListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new WorkerScheduleTaskFilter();
  orderBy?: string;
  orderDirection?: OrderDirection;

  constructor(filter?: WorkerScheduleTaskFilter) {
    this.filter = filter;
  }
}
