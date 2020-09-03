import { LanguageFilter } from './language-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetLanguages {
    pageIndex?: number;
    pageSize?: number;
    filter?: LanguageFilter;
    orderBy?: string;
    orderDirection?: OrderDirection;
}

export class GetLanguage {
    id: number;
}

export class ExportLanguages {
    filter?: LanguageFilter;
    orderBy?: string;
    orderDirection?: OrderDirection;
}

export class UpdateLanguage {
    id: number;
    name: string;
    code: string;
}

export class CreateLanguage {
    name: string;
    code: string;
}

export class DeleteLanguage {
    id: number;
}
