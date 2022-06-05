import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';

import { CommentService } from './comment.service';
import { CommentSelectComponent } from './comment-select.component';
import { CommentShowComponent } from './comment-show.component';

@NgModule({
  declarations: [
    CommentSelectComponent,
    CommentShowComponent
  ],
  imports: [
    CoreModule,
  ],
  providers: [
    CommentService
  ],
  exports: [
    CommentSelectComponent,
    CommentShowComponent
  ]
})
export class CommentCoreModule {

}
