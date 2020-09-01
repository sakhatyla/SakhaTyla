import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { UserService } from './user.service';
import { UserSelectComponent } from './user-select.component';
import { UserShowComponent } from './user-show.component';

@NgModule({
    declarations: [
        UserSelectComponent,
        UserShowComponent
    ],
    imports: [
        CoreModule,
    ],
    providers: [
        UserService
    ],
    exports: [
        UserSelectComponent,
        UserShowComponent
    ]
})
export class UserCoreModule {

}
