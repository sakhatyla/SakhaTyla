import { Component, Input } from '@angular/core';

import { Language } from './language.model';

@Component({
    selector: 'app-language-show',
    templateUrl: './language-show.component.html'
})

export class LanguageShowComponent {
    @Input()
    value: Language;
}
