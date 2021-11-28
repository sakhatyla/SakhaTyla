import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'modelError' })
export class ModelErrorPipe implements PipeTransform {
  transform(value, args: string[]): any {
    if (value && value.errors && value.errors.length > 0) {
      return value.errors[0].errorMessage;
    }
    return null;
  }
}
