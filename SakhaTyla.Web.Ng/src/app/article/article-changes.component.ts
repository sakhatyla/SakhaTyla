import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { filter } from 'rxjs/operators';

import { EntityChangeListState } from '../entity-change-core/entity-change.model';

class DialogData {
    entityName: string;
    entityId: number;
}

@Component({
    selector: 'app-article-changes',
    templateUrl: './article-changes.component.html',
    styleUrls: ['./article-changes.component.scss']
})
export class ArticleChangesComponent implements OnInit {
    entityName: string;
    entityId: number;
    state: EntityChangeListState = new EntityChangeListState();

    constructor(public dialogRef: MatDialogRef<ArticleChangesComponent>,
                @Inject(MAT_DIALOG_DATA) public data: DialogData) {
        this.entityName = data.entityName;
        this.entityId = data.entityId;
    }

    static show(dialog: MatDialog, entityId: number): Observable<any> {
        const dialogRef = dialog.open(ArticleChangesComponent, {
            width: '1200px',
            data: { entityName: 'Article', entityId: entityId }
        });
        return dialogRef.afterClosed()
            .pipe(filter(res => res === true));
    }

    ngOnInit(): void {
    }
}
