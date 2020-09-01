import { Injectable } from '@angular/core';

@Injectable()
export class StoreService {
    map: { [key: string]: any; } = {};

    set(key: string, value: any) {
        this.map[key] = value;
    }

    get(key: string, defaultValue: any = null) {
        let item = this.map[key];
        if (item === undefined || item === null) {
            item = this.map[key] = defaultValue;
        }
        return item;
    }
}
