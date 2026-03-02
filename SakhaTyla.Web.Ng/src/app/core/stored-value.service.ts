import { Injectable } from '@angular/core';
import { StoreService } from './store.service';

type Interceptor<T> = (value: T) => T;

@Injectable()
export class StoredValueService {

  constructor(private storeService: StoreService) {

  }

  getStoredValue<T>(key: string, defaultValue: T, interceptor?: Interceptor<T>): StoredValue<T> {
    return new StoredValue(key, defaultValue, interceptor, this.storeService);
  }
}

export class StoredValue<T> {

  private innerValue: T;

  get value(): T {
    if (this.innerValue === undefined) {
      const value = this.storeService.get(this.key, this.defaultValue);
      this.innerValue = this.getInterceptor ? this.getInterceptor(value) : value;
    }
    return this.innerValue;
  }
  set value(value: T) {
    this.innerValue = value;
    this.storeService.set(this.key, this.innerValue);
  }

  constructor(private key: string, private defaultValue: T, private getInterceptor: Interceptor<T> | undefined, private storeService: StoreService) {

  }
}
