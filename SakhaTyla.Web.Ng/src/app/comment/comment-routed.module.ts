import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CommentsComponent } from './comments.component';
import { CommentModule } from '../comment/comment.module';
import { CommentsViewComponent } from './comments-view.component';

@NgModule({
  declarations: [
  ],
  imports: [
    RouterModule.forChild([
      { path: '', component: CommentsComponent },
      { path: ':containerId', component: CommentsViewComponent }
    ]),
    CommentModule,
  ],
  providers: [
  ],
  entryComponents: [
  ]
})
export class CommentRoutedModule {

}
