import { Component, OnInit } from '@angular/core';

import { StoreService } from '../core/store.service';
import { CategoryListState } from '../category-core/category.model';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent {

  state: CategoryListState;

  constructor(private storeService: StoreService) {
    this.state = this.storeService.get('categoryListState', new CategoryListState());
  }

}
