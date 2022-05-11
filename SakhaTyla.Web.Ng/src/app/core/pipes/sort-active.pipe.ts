import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'sortActive' })
export class SortActivePipe implements PipeTransform {
  transform(value, args: string[]): any {
    if (value && value.orderBy && value.orderDirection !== null && value.orderDirection !== undefined) {
      return value.orderBy.lowerFirstLetter();
    }
    return null;
  }
}
