import { PageSettings } from '../core/page.model';
import { FileGroupFilter } from './file-group-filter.model';
import { FileGroupType } from '../file-group-type/file-group-type.model';

export class FileGroup {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  name: string;
  type: FileGroupType;
  location: string;
  accept: string;
}

export class FileGroupListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new FileGroupFilter();
}
