import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { TagListState } from '../tag-core/tag.model';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.scss']
})
export class TagsComponent {

  state: TagListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('tagListState', new TagListState());
  }

}
