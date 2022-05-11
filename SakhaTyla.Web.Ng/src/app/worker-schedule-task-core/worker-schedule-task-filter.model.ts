import { EntityFilter } from '../core/models/entity-filter.model';

export class WorkerScheduleTaskFilter extends EntityFilter {
  workerInfoId?: number;
  seconds?: string;
  minutes?: string;
  hours?: string;
  dayOfMonth?: string;
  month?: string;
  dayOfWeek?: string;
  year?: string;

  constructor(workerInfoId?: number) {
    super();
    this.workerInfoId = workerInfoId;
  }
}
