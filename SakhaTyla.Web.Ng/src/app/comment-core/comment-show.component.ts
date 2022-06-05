import { Component, Input } from '@angular/core';

import { Comment } from './comment.model';

@Component({
  selector: 'app-comment-show',
  templateUrl: './comment-show.component.html'
})

export class CommentShowComponent {
  @Input()
  value: Comment;
}
