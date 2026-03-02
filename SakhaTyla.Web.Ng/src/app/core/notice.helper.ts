import { Injectable } from '@angular/core';
import { MatLegacySnackBar as MatSnackBar } from '@angular/material/legacy-snack-bar';

import { Error } from './error.model';

@Injectable()
export class NoticeHelper {

  constructor(private snackBar: MatSnackBar) {}

  showError(error: Error) {
    this.snackBar.open(error.message, 'Ok', { duration: 5000 });
  }

  showMessage(message: string) {
    this.snackBar.open(message, 'Ok', { duration: 5000 });
  }
}
