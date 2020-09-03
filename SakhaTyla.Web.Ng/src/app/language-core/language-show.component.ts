import { Component, Input } from '@angular/core';

import { Language } from './language.model';
import { LanguageService } from './language.service';

@Component({
    selector: 'app-language-show',
    templateUrl: './language-show.component.html'
})

export class LanguageShowComponent {
    @Input()
    value: Language;

    @Input()
    set entityId(val: number) {
        this.languageService.getLanguage({ id: val })
            .subscribe(child => this.value = child);
    }

    constructor(private languageService: LanguageService) {}
}
