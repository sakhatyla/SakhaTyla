import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';

import { ArticleService } from '../article-core/article.service';

@Component({
  selector: 'app-article-import',
  templateUrl: './article-import.component.html',
  styleUrls: ['./article-import.component.scss']
})
export class ArticleImportComponent implements OnInit {
  importForm = this.fb.group({
    fromLanguageId: [],
    toLanguageId: [],
    fileId: []
  });
  error: Error;

  constructor(public dialogRef: MatDialogRef<ArticleImportComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {},
    private articleService: ArticleService,
    private fb: FormBuilder,
    private noticeHelper: NoticeHelper) {
  }

  static show(dialog: MatDialog): Observable<any> {
    const dialogRef = dialog.open(ArticleImportComponent, {
      width: '600px',
      data: {}
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
  }

  onImport(): void {
    this.articleService.importArticles(this.importForm.value)
      .subscribe(() => this.dialogRef.close(true),
        error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.importForm, error);
    }
  }
}
