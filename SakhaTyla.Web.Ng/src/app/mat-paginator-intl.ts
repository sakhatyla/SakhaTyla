import { Injectable } from '@angular/core';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { TranslocoService } from '@ngneat/transloco';

@Injectable()
export class MatPaginatorIntlCustom extends MatPaginatorIntl {
  itemsPerPageLabel = this.translocoService.translate('shared.itemsPerPage');
  nextPageLabel     = this.translocoService.translate('shared.nextPage');
  previousPageLabel = this.translocoService.translate('shared.previousPage');
  firstPageLabel    = this.translocoService.translate('shared.firstPage');
  lastPageLabel     = this.translocoService.translate('shared.lastPage');

  constructor(private translocoService: TranslocoService) {
    super();
  }

  getRangeLabel = (page: number, pageSize: number, length: number) => {
    if (length === 0 || pageSize === 0) {
      return `0 of ${length}`;
    }
    length = Math.max(length, 0);
    const startIndex = page * pageSize;
    // If the start index exceeds the list length, do not try and fix the end index to the end.
    const endIndex = startIndex < length ?
      Math.min(startIndex + pageSize, length) :
      startIndex + pageSize;
    return this.translocoService.translate('shared.itemRange', { start: startIndex + 1, end: endIndex, total: length });
  }

}
