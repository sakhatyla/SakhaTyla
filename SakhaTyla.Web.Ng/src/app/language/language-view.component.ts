import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { Language } from '../language-core/language.model';
import { LanguageService } from '../language-core/language.service';
import { LanguageEditComponent } from './language-edit.component';

@Component({
    selector: 'app-language-view',
    templateUrl: './language-view.component.html',
    styleUrls: ['./language-view.component.scss']
})
export class LanguageViewComponent implements OnInit {
    id: number;
    language: Language;

    constructor(private dialog: MatDialog,
                private languageService: LanguageService,
                private route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.route.params.forEach((params: Params) => {
            this.id = ConvertStringTo.number(params.id);
            this.getLanguage();
        });
    }

    private getLanguage() {
        this.languageService.getLanguage({ id: this.id })
            .subscribe(language => this.language = language);
    }

    onEdit() {
        LanguageEditComponent.show(this.dialog, this.id).subscribe(() => {
            this.getLanguage();
        });
    }

    onBack(): void {
        window.history.back();
    }
}
