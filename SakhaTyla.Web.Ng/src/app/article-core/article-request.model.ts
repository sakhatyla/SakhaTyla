import { ArticleFilter } from './article-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetArticles {
    pageIndex?: number;
    pageSize?: number;
    filter?: ArticleFilter;
    orderBy?: string;
    orderDirection?: OrderDirection;
}

export class GetArticle {
    id: number;
}

export class ExportArticles {
    filter?: ArticleFilter;
    orderBy?: string;
    orderDirection?: OrderDirection;
}

export class UpdateArticle {
    id: number;
    title: string;
    textSource: string;
    fromLanguageId: number;
    toLanguageId: number;
    fuzzy: boolean;
    categoryId: number;
}

export class CreateArticle {
    title: string;
    textSource: string;
    fromLanguageId: number;
    toLanguageId: number;
    fuzzy: boolean;
    categoryId: number;
}

export class DeleteArticle {
    id: number;
}
