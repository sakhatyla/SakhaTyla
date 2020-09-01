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
    pageSize = 10;
    pageIndex = 0;
    filter = new FileFilter();
}
