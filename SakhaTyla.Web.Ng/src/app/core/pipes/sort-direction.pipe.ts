import { Pipe, PipeTransform } from '@angular/core';
import { OrderDirectionManager } from '../models/order-direction.model';

@Pipe({ name: 'sortDirection' })
export class SortDirectionPipe implements PipeTransform {
  transform(value, args: string[]): any {
    if (value && value.orderBy && value.orderDirection !== null && value.orderDirection !== undefined) {
      return OrderDirectionManager.getSortByOrderDirection(value.orderDirection);
    }
    return null;
  }
}
