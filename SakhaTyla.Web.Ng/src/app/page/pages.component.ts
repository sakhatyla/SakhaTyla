import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { PageListState } from '../page-core/page.model';

@Component({
  selector: 'app-pages',
  templateUrl: './pages.component.html',
  styleUrls: ['./pages.component.scss']
})
export class PagesComponent {

  state: PageListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('pageListState', new PageListState());
  }

}
