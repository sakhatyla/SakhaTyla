import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { Widget } from '../widget-core/widget.model';
import { WidgetService } from '../widget-core/widget.service';
import { WidgetEditComponent } from './widget-edit.component';

@Component({
  selector: 'app-widget-view',
  templateUrl: './widget-view.component.html',
  styleUrls: ['./widget-view.component.scss']
})
export class WidgetViewComponent implements OnInit {
  id: number;
  widget: Widget;

  constructor(private dialog: MatDialog,
              private widgetService: WidgetService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getWidget();
    });
  }

  private getWidget() {
    this.widgetService.getWidget({ id: this.id })
      .subscribe(widget => this.widget = widget);
  }

  onEdit() {
    WidgetEditComponent.show(this.dialog, this.id).subscribe(() => {
      this.getWidget();
    });
  }

  onBack(): void {
    window.history.back();
  }
}
