﻿import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { Sort } from '@angular/material/sort';
import { forkJoin, of } from 'rxjs';
import { catchError, mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';
import { OrderDirectionManager } from '../core/models/order-direction.model';

import { Category, CategoryListState } from '../category-core/category.model';
import { CategoryService } from '../category-core/category.service';
import { CategoryEditComponent } from './category-edit.component';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent implements OnInit {
  content: Page<Category>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'select',
    'name',
    'action'
  ];
  selectedIds = new Set<number>();

  @Input()
  state: CategoryListState;

  @Input()
  baseRoute = '/category';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private categoryService: CategoryService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getCategories();
  }

  private getCategories() {
    this.selectedIds = new Set<number>();
    this.categoryService.getCategories({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getCategories();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getCategories();
  }

  onCreate() {
    CategoryEditComponent.show(this.dialog, null).subscribe(() => {
      this.getCategories();
    });
  }

  onExport(): void {
    this.categoryService.exportCategories({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    CategoryEditComponent.show(this.dialog, id).subscribe(() => {
      this.getCategories();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.categoryService.deleteCategory({ id }))
      )
      .subscribe(() => this.getCategories(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.categoryService.deleteCategory({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getCategories());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getCategories();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getCategories();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
