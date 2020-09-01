import { EntityFilter } from '../core/models/entity-filter.model';

export class FileFilter extends EntityFilter {
    name?: string;
    contentType?: string;

    url?: string;
    groupId?: number;
}
