import { PageFilter } from './page-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';
import { PageType } from '../page-type/page-type.model';

export class GetPages {
  pageIndex?: number;
  pageSize?: number;
  filter?: PageFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class GetPage {
  id: number;
}

export class ExportPages {
  filter?: PageFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdatePage {
  id: number;
  type: PageType;
  name: string;
  shortName: string;
  parentId: number;
  header: string;
  body: string;
  metaTitle: string;
  metaKeywords: string;
  metaDescription: string;
  imageId: number;
  preview: string;
  publicationDate: Date;
}

export class CreatePage {
  type: PageType;
  name: string;
  shortName: string;
  parentId: number;
  header: string;
  body: string;
  metaTitle: string;
  metaKeywords: string;
  metaDescription: string;
  imageId: number;
  preview: string;
  publicationDate: Date;
}

export class DeletePage {
  id: number;
}
