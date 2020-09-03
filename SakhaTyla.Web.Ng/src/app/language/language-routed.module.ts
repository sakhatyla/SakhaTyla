import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { LanguagesComponent } from './languages.component';
import { LanguageViewComponent } from './language-view.component';
import { LanguageModule } from '../language/language.module';

@NgModule({
    declarations: [
    ],
    imports: [
        RouterModule.forChild([
            { path: '', component: LanguagesComponent },
            { path: ':id', component: LanguageViewComponent }
        ]),
        LanguageModule,
    ],
    providers: [
    ],
    entryComponents: [
    ]
})
export class LanguageRoutedModule {

}
