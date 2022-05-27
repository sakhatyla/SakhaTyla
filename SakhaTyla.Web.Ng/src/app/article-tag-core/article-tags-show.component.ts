import { Component, Input } from '@angular/core';

import { ArticleTag } from './article-tag.model';

@Component({
  selector: 'app-article-tags-show',
  templateUrl: './article-tags-show.component.html'
})

export class ArticleTagsShowComponent {

  @Input()
  set value(tags: ArticleTag[]) {
    this.tags = tags?.map(e => e.tag.name).join(', ');
  }

  tags: string;
}
