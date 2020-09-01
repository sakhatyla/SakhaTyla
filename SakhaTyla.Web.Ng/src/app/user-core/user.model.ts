import { UserFilter } from './user-filter.model';

export class User {
    id: number;
    creationDate: Date;
    modificationDate: Date;
    userName: string;
    email: string;
    emailConfirmed: boolean;
    roleIds: number[];
    firstName: string;
    lastName: string;
}

export class UserListState {
    pageSize = 10;
    pageIndex = 0;
    filter = new UserFilter();
}
