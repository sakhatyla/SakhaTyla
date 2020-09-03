import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { Language } from '../language-core/language.model';
import { LanguageService } from '../language-core/language.service';

class DialogData {
    id: number;
}

@Component({
    selector: 'app-language-edit',
    templateUrl: './language-edit.component.html',
    styleUrls: ['./language-edit.component.scss']
})
export class LanguageEditComponent implements OnInit {
    id: number;
    languageForm = this.fb.group({
        id: [],
        name: [],
        code: []
    });
    language: Language;
    error: Error;

    constructor(public dialogRef: MatDialogRef<LanguageEditComponent>,
                @Inject(MAT_DIALOG_DATA) public data: DialogData,
                private languageService: LanguageService,
                private fb: FormBuilder,
                private noticeHelper: NoticeHelper) {
        this.id = data.id;
    }

    static show(dialog: MatDialog, id: number): Observable<any> {
        const dialogRef = dialog.open(LanguageEditComponent, {
            width: '600px',
            data: { id: id }
        });
        return dialogRef.afterClosed()
            .pipe(filter(res => res === true));
    }

    ngOnInit(): void {
        this.getLanguage();
    }

    private getLanguage() {
        const getLanguage$ = !this.id ?
            of(new Language()) :
            this.languageService.getLanguage({ id: this.id });
        getLanguage$.subscribe(language => {
            this.language = language;
            this.languageForm.patchValue(this.language);
        });
    }

    onSave(): void {
        this.saveLanguage();
    }

    private saveLanguage() {
        const saveLanguage$ = this.id ?
            this.languageService.updateLanguage(this.languageForm.value) :
            this.languageService.createLanguage(this.languageForm.value);
        saveLanguage$.subscribe(() => this.dialogRef.close(true),
            error => this.onError(error));
    }

    onError(error: Error) {
        this.error = error;
        if (error) {
            this.noticeHelper.showError(error);
            Error.setFormErrors(this.languageForm, error);
        }
    }
}
