import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';

@Component({
  selector: 'app-column-settings',
  templateUrl: 'column-settings.component.html',
  styleUrls: ['column-settings.component.scss']
})
export class ColumnSettingsComponent implements OnInit {

  settingColumns = this.formBuilder.group({});

  @Input()
  columns: string[];

  @Output()
  columnsChange = new EventEmitter<string[]>();

  @Input()
  defaultColumns: string[];

  @Input()
  columnDescriptions: ColumnDescription[];

  constructor(
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.columnDescriptions.forEach(columnDescription => {
      if (!this.settingColumns.controls[columnDescription.name]) {
        this.settingColumns.addControl(columnDescription.name, new FormControl(true));
      }
    });
    this.initForm();
  }

  initForm() {
    this.columnDescriptions.forEach(columnDescription => {
      const columnSelected = !!this.columns.find(c => c === columnDescription.name);
      this.settingColumns.controls[columnDescription.name].setValue(columnSelected);
    });
  }

  toDefaultColumns() {
    this.columns = this.defaultColumns;
    this.initForm();
    this.columnsChange.emit(this.columns);
  }

  applySelectedColumns() {
    const columns = Object.entries(this.settingColumns.value);
    const result = [];
    for (const [key, value] of columns) {
      if (value) { result.push(key); }
    }
    this.columns = result;
    this.columnsChange.emit(this.columns);
  }
}

export class ColumnDescription {
  name: string;
  displayName?: string;
  isSystem ? = false;

  static filter(columnDescriptions: ColumnDescription[]): (value: string[]) => string[] {
    return (value: string[]) => value.filter((item) => columnDescriptions.find(cd => cd.name === item));
  }
}

export class ColumnSettingsStore {
  constructor(private key: string, defaultColumns: string[]) {

  }
}
