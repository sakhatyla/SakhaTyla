import { LanguageFilter } from './language-filter.model';

export class Language {
    id: number;
    creationDate: Date;
    modificationDate: Date;
    creationUserId: number;
    modificationUserId: number;
    name: string;
    code: string;
}

export class LanguageListState {
    pageSize = 10;
    pageIndex = 0;
    filter = new LanguageFilter();
}
