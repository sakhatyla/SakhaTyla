import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { TagCoreModule } from '../tag-core/tag-core.module';

import { TagListComponent } from './tag-list.component';
import { TagEditComponent } from './tag-edit.component';
import { TagViewComponent } from './tag-view.component';
import { TagsComponent } from './tags.component';

@NgModule({
  declarations: [
    TagListComponent,
    TagEditComponent,
    TagViewComponent,
    TagsComponent,
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    TagCoreModule
  ],
  exports: [
    TagListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    TagEditComponent
  ]
})
export class TagModule {

}
