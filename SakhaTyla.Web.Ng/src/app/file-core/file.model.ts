import { PageSettings } from '../core/page.model';
import { FileFilter } from './file-filter.model';

export class File {
  id: number;
  creationDate: Date;
  modificationDate: Date;
  creationUserId: number;
  modificationUserId: number;
  name: string;
  contentType: string;
  url: string;
  groupId: number;
}

export class FileListState {
  pageSize = PageSettings.pageSize;
  pageIndex = 0;
  filter = new FileFilter();
}
