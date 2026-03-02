import { Component, Inject } from '@angular/core';
import { MatLegacyDialogRef as MatDialogRef, MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA, MatLegacyDialog as MatDialog } from '@angular/material/legacy-dialog';
import { Observable } from 'rxjs';
import { filter } from 'rxjs/operators';

import { EntityChangeListState } from '../entity-change-core/entity-change.model';

class DialogData {
  entityName: string;
  entityId: number;
}

@Component({
  selector: 'app-entity-changes',
  templateUrl: './entity-changes.component.html',
  styleUrls: ['./entity-changes.component.scss']
})
export class EntityChangesComponent {
  entityName: string;
  entityId: number;
  state: EntityChangeListState = new EntityChangeListState();

  constructor(public dialogRef: MatDialogRef<EntityChangesComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData) {
    this.entityName = data.entityName;
    this.entityId = data.entityId;
  }

  static show(dialog: MatDialog, entityName: string, entityId: number): Observable<any> {
    const dialogRef = dialog.open(EntityChangesComponent, {
      width: '1200px',
      data: { entityName: entityName, entityId: entityId }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

}
