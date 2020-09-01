import { UserFilter } from './user-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetUsers {
    pageIndex?: number;
    pageSize?: number;
    filter?: UserFilter;
    orderBy?: string;
    orderDirection?: OrderDirection;
}

export class GetUser {
    id: number;
}

export class ExportUsers {
    filter?: UserFilter;
    orderBy?: string;
    orderDirection?: OrderDirection;
}

export class UpdateUser {
    id: number;
    email: string;
    password?: string;
    confirmPassword?: string;
    roleIds: number[];
    firstName: string;
    lastName: string;
}

export class CreateUser {
    email: string;
    password?: string;
    confirmPassword?: string;
    roleIds: number[];
    firstName: string;
    lastName: string;
}

export class DeleteUser {
    id: number;
}
