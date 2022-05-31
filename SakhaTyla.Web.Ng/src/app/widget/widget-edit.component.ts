import { Component, Input, OnInit, Inject } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { Observable, of } from 'rxjs';
import { filter } from 'rxjs/operators';

import { Error } from '../core/error.model';
import { NoticeHelper } from '../core/notice.helper';
import { ConvertStringTo } from '../core/converter.helper';

import { Widget } from '../widget-core/widget.model';
import { WidgetService } from '../widget-core/widget.service';

class DialogData {
  id: number;
}

@Component({
  selector: 'app-widget-edit',
  templateUrl: './widget-edit.component.html',
  styleUrls: ['./widget-edit.component.scss']
})
export class WidgetEditComponent implements OnInit {
  id: number;
  widgetForm = this.fb.group({
    id: [],
    name: [],
    code: [],
    body: [],
    type: []
  });
  widget: Widget;
  error: Error;

  constructor(public dialogRef: MatDialogRef<WidgetEditComponent>,
              @Inject(MAT_DIALOG_DATA) public data: DialogData,
              private widgetService: WidgetService,
              private fb: FormBuilder,
              private noticeHelper: NoticeHelper) {
    this.id = data.id;
  }

  static show(dialog: MatDialog, id: number): Observable<any> {
    const dialogRef = dialog.open(WidgetEditComponent, {
      width: '600px',
      data: { id: id }
    });
    return dialogRef.afterClosed()
      .pipe(filter(res => res === true));
  }

  ngOnInit(): void {
    this.getWidget();
  }

  private getWidget() {
    const getWidget$ = !this.id ?
      of(new Widget()) :
      this.widgetService.getWidget({ id: this.id });
    getWidget$.subscribe(widget => {
      this.widget = widget;
      this.widgetForm.patchValue(this.widget);
    });
  }

  onSave(): void {
    this.saveWidget();
  }

  private saveWidget() {
    const saveWidget$ = this.id ?
      this.widgetService.updateWidget(this.widgetForm.value) :
      this.widgetService.createWidget(this.widgetForm.value);
    saveWidget$.subscribe(() => this.dialogRef.close(true),
      error => this.onError(error));
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.widgetForm, error);
    }
  }
}
