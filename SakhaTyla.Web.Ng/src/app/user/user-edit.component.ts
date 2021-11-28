import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';

import { User } from '../user-core/user.model';
import { UserService } from '../user-core/user.service';

class DialogData {
  id: number;
}

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
  id: number;
  userForm = this.fb.group({
    id: [],
    email: [],
    emailConfirmed: [],
    password: [],
    confirmPassword: [],
    firstName: [],
    lastName: [],
    roleIds: []
  });
  user: User;
  error: Error;

  constructor(public dialogRef: MatDialogRef<UserEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private userService: UserService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
  }

  static show(dialog: MatDialog, id: number): Observable<any> {
    const dialogRef = dialog.open(UserEditComponent, {
      width: '600px',
      data: { id: id }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit() {
    this.getUser();
  }

  private getUser() {
    const getUser$ = !this.id ?
      of(new User()) :
      this.userService.getUser({ id: this.id });
    getUser$.subscribe(user => {
      this.user = user;
      this.userForm.patchValue(this.user);
    });
  }

  onSave() {
    this.saveUser();
  }

  private saveUser() {
    const user = this.userForm.value;
    const saveUser$ = this.id ?
      this.userService.updateUser(user) :
      this.userService.createUser(user);
    saveUser$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.userForm, error);
    }
  }
}
