import { Component, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-bool-edit',
  templateUrl: './bool.edit.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => BoolEditComponent),
      multi: true
    }
  ]
})
export class BoolEditComponent implements ControlValueAccessor {

  @Input()
  value: boolean;

  @Input()
  name: string;

  @Input()
  label: string;

  @Input()
  disabled = false;

  get innerValue() {
    return this.value;
  }

  set innerValue(val) {
    if (val === null || val === undefined) {
      this.convertNullToFalse = true;
      this.value = false;
    } else {
      this.value = val;
    }
    this.onChange(this.value);
    this.onTouched();
  }

  private convertNullToFalse = false;

  onChange: any = () => { };
  onTouched: any = () => { };

  registerOnChange(fn) {
    this.onChange = fn;
    if (this.convertNullToFalse) {
      this.onChange(this.value);
    }
  }

  registerOnTouched(fn) {
    this.onTouched = fn;
    if (this.convertNullToFalse) {
      this.onTouched();
    }
  }

  writeValue(value) {
    this.innerValue = value;
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}
