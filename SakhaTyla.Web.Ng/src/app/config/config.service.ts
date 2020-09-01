import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Config } from './config.model';

@Injectable({
    providedIn: 'root'
})
export class ConfigService {

    public config: Config;

    constructor(private http: HttpClient) { }

    public load(): Promise<any> {
        return this.http.get(this.buildUrl())
            .toPromise()
            .then(config => {
                this.config = config as Config;
            });
    }

    private buildUrl(): string {
        let url = 'assets/config.json';
        url += '?t=' + this.makeId();
        return url;
    }

    private makeId(): string {
        let text = '';
        const possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';

        for (let i = 0; i < 5; i++) {
            text += possible.charAt(Math.floor(Math.random() * possible.length));
        }

        return text;
    }
}
