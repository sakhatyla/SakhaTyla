import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { filter } from 'rxjs/operators';
import { TranslocoService } from '@ngneat/transloco';

import { ModalComponent } from './modal.component';


@Injectable()
export class ModalHelper {
  constructor(private dialog: MatDialog,
              private translocoService: TranslocoService) {
  }

  confirmDelete(): Observable<any> {
    const dialogRef = this.dialog.open(ModalComponent, {
      width: '300px',
      data: {
        title: this.translocoService.translate('shared.deleteTitle'),
        body: this.translocoService.translate('shared.deleteMessage'),
        okButton: this.translocoService.translate('shared.delete'),
        cancelButton: this.translocoService.translate('shared.cancel')
      }
    });

    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  confirm(message: string, title: string, okButton: string): Observable<any> {
    const dialogRef = this.dialog.open(ModalComponent, {
      width: '300px',
      data: {
        title: title,
        body: message,
        okButton: okButton,
        cancelButton: this.translocoService.translate('shared.cancel')
      }
    });

    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  alert(message: string, title: string, okButton: string): Observable<any> {
    const dialogRef = this.dialog.open(ModalComponent, {
      width: '300px',
      data: {
        title: title,
        body: message,
        okButton: okButton,
      }
    });

    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }
}
