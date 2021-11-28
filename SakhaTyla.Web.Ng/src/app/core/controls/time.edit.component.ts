import { Component, Input, forwardRef, OnDestroy, ElementRef, Optional, Self, DoCheck } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NgControl } from '@angular/forms';
import { MatFormFieldControl } from '@angular/material/form-field';
import { FocusMonitor } from '@angular/cdk/a11y';
import { coerceBooleanProperty } from '@angular/cdk/coercion';

import { Subject } from 'rxjs';

@Component({
  selector: 'app-time-edit',
  templateUrl: './time.edit.component.html',
  providers: [
    { provide: MatFormFieldControl, useExisting: TimeEditComponent }
  ]
})
export class TimeEditComponent implements ControlValueAccessor, MatFormFieldControl<string>, OnDestroy, DoCheck {

  static nextId = 0;

  stateChanges = new Subject<void>();
  focused = false;
  controlType = 'app-time-edit';
  id = `time-edit-${TimeEditComponent.nextId++}`;
  describedBy = '';

  errorState = false;

  get empty() {
    return !this.value;
  }

  get shouldLabelFloat() { return this.focused || !this.empty; }


  @Input()
  value: string;

  private valueLocal: Date;

  get innerValue(): string {
    if (this.value && !this.valueLocal) {
      this.valueLocal = new Date(`2000-01-01T${this.value}`);
    }
    if (this.valueLocal) {
      return this.valueLocal.toISOString();
    } else {
      return null;
    }
  }
  set innerValue(val: string) {
    if (val) {
      this.valueLocal = new Date(val);
      this.value = this.valueLocal.toTimeString().substring(0, 5);
    } else {
      this.valueLocal = null;
      this.value = null;
    }
    this.onChange(this.value);
    this.onTouched();
  }

  @Input()
  name: string;

  @Input()
  placeholder: string;

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


  onChange: any = () => { };
  onTouched: any = () => { };

  constructor(private fm: FocusMonitor, private elRef: ElementRef<HTMLElement>,
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
    this.value = value;
    this.valueLocal = null;
    this.innerValue = this.innerValue;
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

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}
