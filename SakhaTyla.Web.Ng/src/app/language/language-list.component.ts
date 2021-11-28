import { Component, OnInit, Input } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { mergeMap } from 'rxjs/operators';

import { ModalHelper } from '../core/modal.helper';
import { StoreService } from '../core/store.service';
import { Error } from '../core/error.model';
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';

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
    'name',
    'code',
    'action'
  ];

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
    this.languageService.getLanguages({
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize,
      filter: this.state.filter
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

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getLanguages();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
