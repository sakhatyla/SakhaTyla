import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { FileGroupListState } from '../file-group-core/file-group.model';

@Component({
  selector: 'app-file-groups',
  templateUrl: './file-groups.component.html',
  styleUrls: ['./file-groups.component.scss']
})
export class FileGroupsComponent {

  state: FileGroupListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('fileGroupListState', new FileGroupListState());
  }

}
