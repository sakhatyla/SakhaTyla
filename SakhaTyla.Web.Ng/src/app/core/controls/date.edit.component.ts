import { Component, Input, forwardRef, OnDestroy, ElementRef, Optional, Self, DoCheck } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NgControl } from '@angular/forms';
import { MatFormFieldControl } from '@angular/material/form-field';
import { FocusMonitor } from '@angular/cdk/a11y';
import { coerceBooleanProperty } from '@angular/cdk/coercion';

import { Subject } from 'rxjs';

@Component({
    selector: 'app-date-edit',
    templateUrl: './date.edit.component.html',
    providers: [
        { provide: MatFormFieldControl, useExisting: DateEditComponent }
    ]
})
export class DateEditComponent implements ControlValueAccessor, MatFormFieldControl<Date>, OnDestroy, DoCheck {

    static nextId = 0;

    stateChanges = new Subject<void>();
    focused = false;
    controlType = 'app-date-edit';
    id = `date-edit-${DateEditComponent.nextId++}`;
    describedBy = '';

    errorState = false;

    get empty() {
        return !this.value;
    }

    get shouldLabelFloat() { return this.focused || !this.empty; }

    @Input()
    value: Date;

    @Input()
    name: string;

    @Input()
    placeholder: string;

    @Input()
    type = 'text';

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

    get innerValue(): string {
        if (this.value) {
            return this.value.toISOString();
        } else {
            return null;
        }
    }

    set innerValue(val: string) {
        if (val) {
            this.value = this.removeTimezone(new Date(val));
        } else {
            this.value = null;
        }
        this.onChange(this.value);
        this.onTouched();
    }

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
    removeTimezone(date: Date): Date {
        return new Date(date.getTime() - date.getTimezoneOffset() * 60 * 1000);
    }

    registerOnChange(fn) {
        this.onChange = fn;
    }

    registerOnTouched(fn) {
        this.onTouched = fn;
    }

    writeValue(value) {
        this.innerValue = value;
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
