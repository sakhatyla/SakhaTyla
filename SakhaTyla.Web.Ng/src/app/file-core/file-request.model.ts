import { FileFilter } from './file-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetFiles {
  pageIndex?: number;
  pageSize?: number;
  filter?: FileFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetFile {
  id: number;
}

export class DownloadFile {
  id: number;
}

export class ExportFiles {
  filter?: FileFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateFile {
  id: number;
  file: File;
}

export class CreateFile {
  file: File;
  groupId: number;
}

export class DeleteFile {
  id: number;
}
