import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { Page } from '../page-core/page.model';
import { PageService } from '../page-core/page.service';

class DialogData {
  id: number;
}

@Component({
  selector: 'app-page-edit',
  templateUrl: './page-edit.component.html',
  styleUrls: ['./page-edit.component.scss']
})
export class PageEditComponent implements OnInit {
  id: number;
  pageForm = this.fb.group({
    id: [],
    type: [],
    name: [],
    route: this.fb.group({
      path: [],
    }),
    shortName: [],
    parentId: [],
    header: [],
    body: [],
    metaTitle: [],
    metaKeywords: [],
    metaDescription: [],
    imageId: [],
    preview: []
  });
  page: Page;
  error: Error;

  constructor(public dialogRef: MatDialogRef<PageEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private pageService: PageService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
  }

  static show(dialog: MatDialog, id: number): Observable<any> {
    const dialogRef = dialog.open(PageEditComponent, {
      width: '600px',
      data: { id: id }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getPage();
  }

  private getPage() {
    const getPage$ = !this.id ?
      of(new Page()) :
      this.pageService.getPage({ id: this.id });
    getPage$.subscribe(page => {
      this.page = page;
      this.pageForm.patchValue(this.page);
    });
  }

  onSave(): void {
    this.savePage();
  }

  private savePage() {
    const savePage$ = this.id ?
      this.pageService.updatePage(this.pageForm.value) :
      this.pageService.createPage(this.pageForm.value);
    savePage$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.pageForm, error);
    }
  }
}
