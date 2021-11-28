import { Component, Input, OnInit, forwardRef, OnDestroy, ElementRef, Optional, Self, DoCheck } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NgControl } from '@angular/forms';
import { MatFormFieldControl } from '@angular/material/form-field';
import { MatSnackBar } from '@angular/material/snack-bar';
import { FocusMonitor } from '@angular/cdk/a11y';
import { coerceBooleanProperty } from '@angular/cdk/coercion';
import { Subject } from 'rxjs';

import { File } from './file.model';
import { FileService } from './file.service';
import { FileGroupService } from '../file-group-core/file-group.service';
import { FileGroup } from '../file-group-core/file-group.model';

@Component({
  selector: 'app-file-select',
  templateUrl: './file-select.component.html',
  styleUrls: ['./file-select.component.scss'],
  providers: [
    { provide: MatFormFieldControl, useExisting: FileSelectComponent }
  ]
})

export class FileSelectComponent implements OnInit, ControlValueAccessor,
  MatFormFieldControl<number | null>, OnDestroy, DoCheck {

  static nextId = 0;

  stateChanges = new Subject<void>();
  focused = false;
  controlType = 'app-file-select';
  id = `file-select-${FileSelectComponent.nextId++}`;
  describedBy = '';

  errorState = false;

  group: FileGroup;
  file: File;

  get empty() {
    return !this.value;
  }

  get shouldLabelFloat() { return this.focused || !this.empty; }

  File = File;

  @Input()
  value: number | null = null;

  @Input()
  name: string;

  @Input()
  placeholder: string;

  @Input()
  groupName: string;

  @Input()
  get required(): boolean { return this.innerRequired; }
  set required(value: boolean) {
    this.innerRequired = coerceBooleanProperty(value);
    this.stateChanges.next();
  }
  private innerRequired = false;

  @Input()
  get disabled(): boolean { return this.innerDisabled; }
  set disabled(value: boolean) {
    this.innerDisabled = coerceBooleanProperty(value);
    this.stateChanges.next();
  }
  private innerDisabled = false;

  get innerValue() {
    return this.value;
  }

  set innerValue(val) {
    this.value = val;
    this.onChange(val);
    this.onTouched();
  }

  onChange: any = () => { };
  onTouched: any = () => { };

  constructor(private fileService: FileService,
              private fileGroupService: FileGroupService,
              private snackBar: MatSnackBar,
              private fm: FocusMonitor, private elRef: ElementRef<HTMLElement>,
              @Optional() @Self() public ngControl: NgControl) {
    fm.monitor(elRef, true).subscribe(origin => {
      this.focused = !!origin;
      this.stateChanges.next();
    });
    if (this.ngControl !== null) {
      this.ngControl.valueAccessor = this;
    }
  }

  registerOnChange(fn) {
    this.onChange = fn;
  }

  registerOnTouched(fn) {
    this.onTouched = fn;
  }

  writeValue(value) {
    this.innerValue = value;
    if (this.value) {
      this.getFile();
    }
  }

  ngOnInit(): void {
    this.fileGroupService.getFileGroup({ name: this.groupName }).subscribe(group => this.group = group);
  }

  private getFile() {
    this.fileService.getFile({ id: this.value }).subscribe(file => this.file = file);
  }

  ngOnDestroy() {
    this.stateChanges.complete();
    this.fm.stopMonitoring(this.elRef);
  }

  setDescribedByIds(ids: string[]) {
    this.describedBy = ids.join(' ');
  }

  onContainerClick(event: MouseEvent) {
  }

  ngDoCheck(): void {
    if (this.ngControl) {
      this.errorState = this.ngControl.invalid;
      this.stateChanges.next();
    }
  }

  onFileChange(event: EventTarget) {
    const eventObj = event as MSInputMethodContext;
    const input = eventObj.target as HTMLInputElement;
    const file = input.files[0];
    if (file) {
      this.fileService.createFile({ groupId: this.group.id, file: input.files[0] })
      .subscribe((createdFile) => {
        this.innerValue = createdFile.id;
        this.getFile();
      }, error => {
        this.onError(error);
      });
    }
  }

  onDelete() {
    this.file = null;
    this.innerValue = null;
  }

  onError(error: Error) {
    if (error) {
      this.snackBar.open(error.message, 'Ok');
    }
  }
}
