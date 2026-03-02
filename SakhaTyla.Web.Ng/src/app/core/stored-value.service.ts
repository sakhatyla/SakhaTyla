import { Injectable } from '@angular/core';
import { StoreService } from './store.service';

@Injectable()
export class StoredValueService {

  constructor(private storeService: StoreService) {

  }

  getStoredValue<T>(key: string, defaultValue: T): StoredValue<T> {
    return new StoredValue(key, defaultValue, this.storeService);
  }
}

export class StoredValue<T> {

  private innerValue: T;

  get value(): T {
    if (this.innerValue === undefined) {
      this.innerValue = this.storeService.get(this.key, this.defaultValue);
    }
    return this.innerValue;
  }
  set value(value: T) {
    this.innerValue = value;
    this.storeService.set(this.key, this.innerValue);
  }

  constructor(private key: string, private defaultValue: T, private storeService: StoreService) {

  }
}
