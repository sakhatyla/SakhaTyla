import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { TagsComponent } from './tags.component';
import { TagViewComponent } from './tag-view.component';
import { TagModule } from '../tag/tag.module';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: TagsComponent },
      { path: ':id', component: TagViewComponent }
    ]),
    TagModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class TagRoutedModule {

}
