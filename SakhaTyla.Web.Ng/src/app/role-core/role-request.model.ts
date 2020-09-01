import { RoleFilter } from './role-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetRoles {
    pageIndex?: number;
    pageSize?: number;
    filter?: RoleFilter;
    orderBy?: string;
    orderDirection?: OrderDirection;
}

export class GetRole {
    id: number;
}

export class ExportRoles {
    filter?: RoleFilter;
    orderBy?: string;
    orderDirection?: OrderDirection;
}

export class UpdateRole {
    id: number;
    name: string;
    displayName: string;
}

export class CreateRole {
    name: string;
    displayName: string;
}

export class DeleteRole {
    id: number;
}
