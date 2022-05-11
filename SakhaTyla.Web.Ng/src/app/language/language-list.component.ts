import { Component, OnInit, Input } from '@angular/core';
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

import { Language, LanguageListState } from '../language-core/language.model';
import { LanguageService } from '../language-core/language.service';
import { LanguageEditComponent } from './language-edit.component';

@Component({
  selector: 'app-language-list',
  templateUrl: './language-list.component.html',
  styleUrls: ['./language-list.component.scss']
})
export class LanguageListComponent implements OnInit {
  content: Page<Language>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  columns = [
    'select',
    'name',
    'code',
    'action'
  ];
  selectedIds = new Set<number>();

  @Input()
  state: LanguageListState;

  @Input()
  baseRoute = '/language';

  constructor(
    private dialog: MatDialog,
    private modalHelper: ModalHelper,
    private languageService: LanguageService,
    private noticeHelper: NoticeHelper
    ) {
  }

  ngOnInit() {
    this.getLanguages();
  }

  private getLanguages() {
    this.selectedIds = new Set<number>();
    this.languageService.getLanguages({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter,
      orderBy: this.state.orderBy,
      orderDirection: this.state.orderDirection
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.state.pageIndex = 0;
    this.getLanguages();
  }

  onReset() {
    this.state.pageIndex = 0;
    this.state.filter.text = null;
    this.getLanguages();
  }

  onCreate() {
    LanguageEditComponent.show(this.dialog, null).subscribe(() => {
      this.getLanguages();
    });
  }

  onExport(): void {
    this.languageService.exportLanguages({ filter: this.state.filter })
      .subscribe(file => {
        file.download();
      });
  }

  onEdit(id: number) {
    LanguageEditComponent.show(this.dialog, id).subscribe(() => {
      this.getLanguages();
    });
  }

  onDelete(id: number) {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => this.languageService.deleteLanguage({ id }))
      )
      .subscribe(() => this.getLanguages(),
        error => this.onError(error));
  }

  onDeleteSelected() {
    this.modalHelper.confirmDelete()
      .pipe(
        mergeMap(() => forkJoin([...this.selectedIds]
          .map(id => this.languageService.deleteLanguage({ id })
            .pipe(
              catchError(error => { this.onError(error); return of({}); })
            ))))
      )
      .subscribe(() => this.getLanguages());
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getLanguages();
  }

  onSortChange(sortState: Sort) {
    this.state.orderDirection = OrderDirectionManager.getOrderDirectionBySort(sortState);
    this.state.orderBy = OrderDirectionManager.getOrderByBySort(sortState);
    this.getLanguages();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
