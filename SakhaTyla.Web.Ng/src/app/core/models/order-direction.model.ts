import { Sort } from '@angular/material/sort';

export enum OrderDirection {
  Ascending,
  Descending
}

export class OrderDirectionManager {
  static getOrderDirectionBySort(sortState: Sort) {
    if (sortState.active) {
      if (sortState.direction === 'desc') {
        return OrderDirection.Descending;
      } else if (sortState.direction === 'asc') {
        return OrderDirection.Ascending;
      }
    }
    return null;
  }

  static getOrderByBySort(sortState: Sort) {
    if (sortState.active && sortState.direction) {
      return sortState.active.capitalizeFirstLetter();
    }
    return null;
  }

  static getSortByOrderDirection(orderDirection: OrderDirection) {
    if (orderDirection === OrderDirection.Descending) {
      return 'desc';
    }
    return 'asc';
  }
}
