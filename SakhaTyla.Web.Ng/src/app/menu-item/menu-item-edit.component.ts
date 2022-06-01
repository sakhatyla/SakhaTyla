import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { MenuItem } from '../menu-item-core/menu-item.model';
import { MenuItemService } from '../menu-item-core/menu-item.service';

class DialogData {
  id: number;
  menuId: number;
}

@Component({
  selector: 'app-menu-item-edit',
  templateUrl: './menu-item-edit.component.html',
  styleUrls: ['./menu-item-edit.component.scss']
})
export class MenuItemEditComponent implements OnInit {
  id: number;
  menuId: number;
  menuItemForm = this.fb.group({
    id: [],
    menuId: [],
    name: [],
    url: [],
    weight: [],
    parentId: []
  });
  menuItem: MenuItem;
  error: Error;

  constructor(public dialogRef: MatDialogRef<MenuItemEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private menuItemService: MenuItemService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
    this.menuId = data.menuId;
  }

  static show(dialog: MatDialog, id: number, menuId: number): Observable<any> {
    const dialogRef = dialog.open(MenuItemEditComponent, {
      width: '600px',
      data: { id: id, menuId: menuId }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getMenuItem();
  }

  private getMenuItem() {
    const getMenuItem$ = !this.id ?
      of(new MenuItem()) :
      this.menuItemService.getMenuItem({ id: this.id });
    getMenuItem$.subscribe(menuItem => {
      this.menuItem = menuItem;
      this.menuItemForm.patchValue(this.menuItem);
    });
  }

  onSave(): void {
    this.saveMenuItem();
  }

  private saveMenuItem() {
    const menuItem = this.menuItemForm.value;
    menuItem.menuId = this.menuId;
    const saveMenuItem$ = this.id ?
      this.menuItemService.updateMenuItem(menuItem) :
      this.menuItemService.createMenuItem(menuItem);
    saveMenuItem$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.menuItemForm, error);
    }
  }
}
