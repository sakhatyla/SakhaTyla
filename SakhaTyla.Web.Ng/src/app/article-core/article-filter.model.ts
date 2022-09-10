import { EntityFilter } from '../core/models/entity-filter.model';

export class ArticleFilter extends EntityFilter {
    title?: string;
    fromLanguageId?: number;
    toLanguageId?: number;
    fuzzy?: boolean;
    categoryId?: number;
    tagId?: number;
}
