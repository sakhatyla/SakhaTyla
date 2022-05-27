import { Component, Input } from '@angular/core';

import { Tag } from './tag.model';

@Component({
  selector: 'app-tag-show',
  templateUrl: './tag-show.component.html'
})

export class TagShowComponent {
  @Input()
  value: Tag;
}
