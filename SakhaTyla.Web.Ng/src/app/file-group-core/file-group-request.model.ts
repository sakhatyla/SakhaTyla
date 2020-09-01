import { FileGroupFilter } from './file-group-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';
import { FileGroupType } from '../file-group-type/file-group-type.model';

export class GetFileGroups {
    pageIndex?: number;
    pageSize?: number;
    filter?: FileGroupFilter;
    orderBy?: string;
    orderDirection?: OrderDirection;
}

export class GetFileGroup {
    id?: number;
    name?: string;
}

export class ExportFileGroups {
    filter?: FileGroupFilter;
    orderBy?: string;
    orderDirection?: OrderDirection;
}

export class UpdateFileGroup {
    id: number;
    name: string;
    type: FileGroupType;
    location: string;
    accept: string;
}

export class CreateFileGroup {
    name: string;
    type: FileGroupType;
    location: string;
    accept: string;
}

export class DeleteFileGroup {
    id: number;
}
