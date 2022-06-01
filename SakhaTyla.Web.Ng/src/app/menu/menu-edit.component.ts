import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { Menu } from '../menu-core/menu.model';
import { MenuService } from '../menu-core/menu.service';

class DialogData {
  id: number;
}

@Component({
  selector: 'app-menu-edit',
  templateUrl: './menu-edit.component.html',
  styleUrls: ['./menu-edit.component.scss']
})
export class MenuEditComponent implements OnInit {
  id: number;
  menuForm = this.fb.group({
    id: [],
    name: [],
    code: []
  });
  menu: Menu;
  error: Error;

  constructor(public dialogRef: MatDialogRef<MenuEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private menuService: MenuService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
  }

  static show(dialog: MatDialog, id: number): Observable<any> {
    const dialogRef = dialog.open(MenuEditComponent, {
      width: '600px',
      data: { id: id }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getMenu();
  }

  private getMenu() {
    const getMenu$ = !this.id ?
      of(new Menu()) :
      this.menuService.getMenu({ id: this.id });
    getMenu$.subscribe(menu => {
      this.menu = menu;
      this.menuForm.patchValue(this.menu);
    });
  }

  onSave(): void {
    this.saveMenu();
  }

  private saveMenu() {
    const saveMenu$ = this.id ?
      this.menuService.updateMenu(this.menuForm.value) :
      this.menuService.createMenu(this.menuForm.value);
    saveMenu$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.menuForm, error);
    }
  }
}
