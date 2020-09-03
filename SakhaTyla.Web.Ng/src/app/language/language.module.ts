import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { LanguageCoreModule } from '../language-core/language-core.module';

import { LanguageListComponent } from './language-list.component';
import { LanguageEditComponent } from './language-edit.component';
import { LanguageViewComponent } from './language-view.component';
import { LanguagesComponent } from './languages.component';

@NgModule({
    declarations: [
        LanguageListComponent,
        LanguageEditComponent,
        LanguageViewComponent,
        LanguagesComponent,
    ],
    imports: [
        RouterModule,
        CoreModule,
        TranslocoRootModule,
        LanguageCoreModule
    ],
    exports: [
        LanguageListComponent,
    ],
    providers: [
    ],
    entryComponents: [
        LanguageEditComponent
    ]
})
export class LanguageModule {

}
