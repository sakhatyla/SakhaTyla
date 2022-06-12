import { EntityFilter } from '../core/models/entity-filter.model';
import { PageType } from '../page-type/page-type.model';

export class PageFilter extends EntityFilter {
  type?: PageType;
  name?: string;
  shortName?: string;
  parentId?: number;
  header?: string;
  body?: string;
  metaTitle?: string;
  metaKeywords?: string;
  metaDescription?: string;
  imageId?: number;
  preview?: string;
  commentContainerId?: number;
  publicationDateFrom?: Date;
  publicationDateTo?: Date;
}
