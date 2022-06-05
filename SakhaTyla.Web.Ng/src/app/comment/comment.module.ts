import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CoreModule } from '../core/core.module';
import { TranslocoRootModule } from '../transloco-root.module';
import { CommentCoreModule } from '../comment-core/comment-core.module';
import { UserCoreModule } from '../user-core/user-core.module';

import { CommentListComponent } from './comment-list.component';
import { CommentEditComponent } from './comment-edit.component';
import { CommentsComponent } from './comments.component';
import { CommentsViewComponent } from './comments-view.component';

@NgModule({
  declarations: [
    CommentListComponent,
    CommentEditComponent,
    CommentsComponent,
    CommentsViewComponent
  ],
  imports: [
    RouterModule,
    CoreModule,
    TranslocoRootModule,
    UserCoreModule,
    CommentCoreModule
  ],
  exports: [
    CommentListComponent,
  ],
  providers: [
  ],
  entryComponents: [
    CommentEditComponent
  ]
})
export class CommentModule {

}
