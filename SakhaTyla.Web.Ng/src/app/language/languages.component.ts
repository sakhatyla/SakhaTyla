import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { LanguageListState } from '../language-core/language.model';

@Component({
    selector: 'app-languages',
    templateUrl: './languages.component.html',
    styleUrls: ['./languages.component.scss']
})
export class LanguagesComponent {

    state: LanguageListState;

    constructor(private storeService: StoreService) {
        this.state = this.storeService.get('languageListState', new LanguageListState());
    }

}
