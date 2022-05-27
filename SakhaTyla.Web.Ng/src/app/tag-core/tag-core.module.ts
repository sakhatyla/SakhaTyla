import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { TagService } from './tag.service';
import { TagSelectComponent } from './tag-select.component';
import { TagShowComponent } from './tag-show.component';

@NgModule({
  declarations: [
    TagSelectComponent,
    TagShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    TagService
  ],
  exports: [
    TagSelectComponent,
    TagShowComponent
  ]
})
export class TagCoreModule {

}
