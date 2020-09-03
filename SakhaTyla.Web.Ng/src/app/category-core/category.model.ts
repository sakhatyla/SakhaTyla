import { CategoryFilter } from './category-filter.model';

export class Category {
    id: number;
    creationDate: Date;
    modificationDate: Date;
    creationUserId: number;
    modificationUserId: number;
    name: string;
}

export class CategoryListState {
    pageSize = 10;
    pageIndex = 0;
    filter = new CategoryFilter();
}
