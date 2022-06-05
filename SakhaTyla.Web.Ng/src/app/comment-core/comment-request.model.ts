import { CommentFilter } from './comment-filter.model';
import { OrderDirection } from '../core/models/order-direction.model';

export class GetComments {
  pageIndex?: number;
  pageSize?: number;
  filter?: CommentFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
  skipChildren?: boolean;
}

export class GetComment {
  id: number;
}

export class GetCommentContainer {
  id: number;
}

export class ExportComments {
  filter?: CommentFilter;
  orderBy?: string;
  orderDirection?: OrderDirection;
}

export class UpdateComment {
  id: number;
  textSource: string;
}

export class CreateComment {
  containerId: number;
  textSource: string;
  authorId: number;
  parentId: number;
}

export class DeleteComment {
  id: number;
}
