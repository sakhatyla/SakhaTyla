export class Page<T> {
  pageItems: T[];
  totalItems: number;
  currentPageIndex: number;
}

export class PageSettings {
  static readonly pageSize = 10;
  static readonly pageSizeOptions = [10, 20, 50, 100];
}
