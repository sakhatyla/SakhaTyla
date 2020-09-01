import { RoleFilter } from './role-filter.model';

export class Role {
    id: number;
    creationDate: Date;
    modificationDate: Date;
    name: string;
    isSelected: boolean;
    displayName: string;
}

export class RoleListState {
    pageSize = 10;
    pageIndex = 0;
    filter = new RoleFilter();
}
