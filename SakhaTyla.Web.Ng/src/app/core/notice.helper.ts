import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

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
