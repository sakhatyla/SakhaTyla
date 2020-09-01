import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ConfigService } from '../config/config.service';

import { Profile } from './profile.model';
import { GetProfile, UpdateProfile } from './profile-request.model';

@Injectable()
export class ProfileService {
    private apiUrl = this.configService.config.apiBaseUrl + '/api';

    constructor(private httpClient: HttpClient,
                private configService: ConfigService) {
    }

    getProfile(getProfile: GetProfile): Observable<Profile> {
        const url = `${this.apiUrl}/GetProfile`;
        return this.httpClient.post<Profile>(url, getProfile);
    }

    updateProfile(updateProfile: UpdateProfile): Observable<{}> {
        const url = `${this.apiUrl}/UpdateProfile`;
        return this.httpClient.post(url, updateProfile);
    }
}
