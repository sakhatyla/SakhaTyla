import { EntityFilter } from '../core/models/entity-filter.model';

export class CommentFilter extends EntityFilter {
  containerId?: number;
  text?: string;
  textSource?: string;
  authorId?: number;
  parentId?: number;
}
