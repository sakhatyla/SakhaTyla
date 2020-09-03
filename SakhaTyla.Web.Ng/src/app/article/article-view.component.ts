import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { Article } from '../article-core/article.model';
import { ArticleService } from '../article-core/article.service';
import { ArticleEditComponent } from './article-edit.component';

@Component({
    selector: 'app-article-view',
    templateUrl: './article-view.component.html',
    styleUrls: ['./article-view.component.scss']
})
export class ArticleViewComponent implements OnInit {
    id: number;
    article: Article;

    constructor(private dialog: MatDialog,
                private articleService: ArticleService,
                private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.route.params.forEach((params: Params) => {
            this.id = ConvertStringTo.number(params.id);
            this.getArticle();
        });
    }

    private getArticle() {
        this.articleService.getArticle({ id: this.id })
            .subscribe(article => this.article = article);
    }

    onEdit() {
        ArticleEditComponent.show(this.dialog, this.id).subscribe(() => {
            this.getArticle();
        });
    }

    onBack(): void {
        window.history.back();
    }
}
