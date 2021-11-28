import { EntityFilter } from '../core/models/entity-filter.model';
import { WorkerRunStatus } from '../worker-run-status/worker-run-status.model';

export class WorkerRunFilter extends EntityFilter {
  workerInfoId?: number;
  status?: WorkerRunStatus;
  startDateTimeFrom?: Date;
  startDateTimeTo?: Date;
  endDateTimeFrom?: Date;
  endDateTimeTo?: Date;
  data?: string;
  result?: string;
  resultData?: string;
}
