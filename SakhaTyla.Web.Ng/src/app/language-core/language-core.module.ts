import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { LanguageService } from './language.service';
import { LanguageSelectComponent } from './language-select.component';
import { LanguageShowComponent } from './language-show.component';

@NgModule({
    declarations: [
        LanguageSelectComponent,
        LanguageShowComponent
    ],
    imports: [
        CoreModule,
    ],
    providers: [
        LanguageService
    ],
    exports: [
        LanguageSelectComponent,
        LanguageShowComponent
    ]
})
export class LanguageCoreModule {

}
