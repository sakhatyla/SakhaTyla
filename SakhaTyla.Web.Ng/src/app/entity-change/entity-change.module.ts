import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { EntityChangeCoreModule } from '../entity-change-core/entity-change-core.module';
import { UserCoreModule } from '../user-core/user-core.module';
import { ChangeActionModule } from '../change-action/change-action.module';

import { EntityChangeListComponent } from './entity-change-list.component';
import { EntityChangeValueDirective } from './entity-change-value.directive';
import { EntityChangesComponent } from './entity-changes.component';

@NgModule({
    declarations: [
        EntityChangeListComponent,
        EntityChangesComponent,
        EntityChangeValueDirective,
    ],
    imports: [
        RouterModule,
        CoreModule,
        TranslocoRootModule,
        UserCoreModule,
        ChangeActionModule,
        EntityChangeCoreModule
    ],
    exports: [
        EntityChangeListComponent,
        EntityChangeValueDirective,
    ],
    providers: [
    ],
    entryComponents: [
        EntityChangesComponent
    ]
})
export class EntityChangeModule {

}
