import { EntityFilter } from '../core/models/entity-filter.model';
import { FileGroupType } from '../file-group-type/file-group-type.model';

export class FileGroupFilter extends EntityFilter {
  name?: string;
  type?: FileGroupType;
  location?: string;
  accept?: string;
}
