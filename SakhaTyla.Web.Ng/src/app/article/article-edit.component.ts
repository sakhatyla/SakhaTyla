import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { Article } from '../article-core/article.model';
import { ArticleService } from '../article-core/article.service';

class DialogData {
    id: number;
}

@Component({
    selector: 'app-article-edit',
    templateUrl: './article-edit.component.html',
    styleUrls: ['./article-edit.component.scss']
})
export class ArticleEditComponent implements OnInit {
    id: number;
    articleForm = this.fb.group({
        id: [],
        title: [],
        textSource: [],
        fromLanguageId: [],
        toLanguageId: [],
        fuzzy: [],
        categoryId: [],
        tagIds: []
    });
    article: Article;
    error: Error;

    constructor(public dialogRef: MatDialogRef<ArticleEditComponent>,
                @Inject(MAT_DIALOG_DATA) public data: DialogData,
                private articleService: ArticleService,
                private fb: FormBuilder,
                private noticeHelper: NoticeHelper) {
        this.id = data.id;
    }

    static show(dialog: MatDialog, id: number): Observable<any> {
        const dialogRef = dialog.open(ArticleEditComponent, {
            width: '600px',
            data: { id: id }
        });
        return dialogRef.afterClosed()
            .pipe(filter(res => res === true));
    }

    ngOnInit(): void {
        this.getArticle();
    }

    private getArticle() {
        const getArticle$ = !this.id ?
            of(new Article()) :
            this.articleService.getArticle({ id: this.id });
        getArticle$.subscribe(article => {
            this.article = article;
            this.articleForm.patchValue(this.article);
            this.articleForm.controls.tagIds.setValue(this.article.tags?.map(e => e.tagId));
        });
    }

    onSave(): void {
        this.saveArticle();
    }

    private saveArticle() {
        const saveArticle$ = this.id ?
            this.articleService.updateArticle(this.articleForm.value) :
            this.articleService.createArticle(this.articleForm.value);
        saveArticle$.subscribe(() => this.dialogRef.close(true),
            error => this.onError(error));
    }

    onError(error: Error) {
        this.error = error;
        if (error) {
            this.noticeHelper.showError(error);
            Error.setFormErrors(this.articleForm, error);
        }
    }
}
