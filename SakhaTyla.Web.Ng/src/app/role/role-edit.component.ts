import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { Role } from '../role-core/role.model';
import { RoleService } from '../role-core/role.service';

class DialogData {
  id: number;
}

@Component({
  selector: 'app-role-edit',
  templateUrl: './role-edit.component.html',
  styleUrls: ['./role-edit.component.scss']
})
export class RoleEditComponent implements OnInit {
  id: number;
  roleForm = this.fb.group({
    id: [],
    name: [],
    displayName: []
  });
  role: Role;
  error: Error;

  constructor(public dialogRef: MatDialogRef<RoleEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private roleService: RoleService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
  }

  static show(dialog: MatDialog, id: number): Observable<any> {
    const dialogRef = dialog.open(RoleEditComponent, {
      width: '600px',
      data: { id: id }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getRole();
  }

  private getRole() {
    const getRole$ = !this.id ?
      of(new Role()) :
      this.roleService.getRole({ id: this.id });
    getRole$.subscribe(role => {
      this.role = role;
      this.roleForm.patchValue(this.role);
    });
  }

  onSave(): void {
    this.saveRole();
  }

  private saveRole() {
    const saveRole$ = this.id ?
      this.roleService.updateRole(this.roleForm.value) :
      this.roleService.createRole(this.roleForm.value);
    saveRole$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.roleForm, error);
    }
  }
}
