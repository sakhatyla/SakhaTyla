import { Component, Input, OnInit, forwardRef, OnDestroy, ElementRef, Optional, Self, DoCheck } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NgControl, FormControl } from '@angular/forms';
import { MatFormFieldControl } from '@angular/material/form-field';
import { FocusMonitor } from '@angular/cdk/a11y';
import { coerceBooleanProperty } from '@angular/cdk/coercion';
import { Subject, Observable, of } from 'rxjs';
import { filter, debounceTime, mergeMap, map, startWith } from 'rxjs/operators';

import { Role } from './role.model';
import { RoleService } from './role.service';

@Component({
    selector: 'app-role-select',
    templateUrl: './role-select.component.html',
    providers: [
        { provide: MatFormFieldControl, useExisting: RoleSelectComponent }
    ]
})

export class RoleSelectComponent implements OnInit, ControlValueAccessor,
    MatFormFieldControl<number | null>, OnDestroy, DoCheck {

    static nextId = 0;

    stateChanges = new Subject<void>();
    focused = false;
    controlType = 'app-role-select';
    id = `role-select-${RoleSelectComponent.nextId++}`;
    describedBy = '';

    errorState = false;

    get empty() {
        return !this.value;
    }

    get shouldLabelFloat() { return this.focused || !this.empty || this.autocompleteControl.value; }

    Role = Role;

    roles: Role[] = [];

    autocompleteRoles: Observable<Role[]>;

    autocompleteControl = new FormControl();

    @Input()
    value: number | null = null;

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
    mode: 'select' | 'autocomplete' = 'select';

    @Input()
    get disabled(): boolean { return this.innerDisabled; }
    set disabled(value: boolean) {
        this.innerDisabled = coerceBooleanProperty(value);
        if (value) {
            this.autocompleteControl.disable();
        } else {
            this.autocompleteControl.enable();
        }
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

    constructor(private roleService: RoleService,
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
        if (this.mode === 'autocomplete') {
            if (this.value) {
                this.roleService.getRole({ id: this.value })
                    .subscribe(role => this.autocompleteControl.setValue(role));
            } else {
                this.autocompleteControl.setValue('');
            }
        }
    }

    ngOnInit(): void {
        if (this.mode === 'select') {
            this.roleService.getRoles({}).subscribe(roles => this.roles = roles.pageItems);
        } else if (this.mode === 'autocomplete') {
            this.autocompleteRoles = this.autocompleteControl.valueChanges
                .pipe(
                    map(value => {
                        if (value === '') {
                            this.innerValue = null;
                            return value;
                        } else if (typeof value === 'string') {
                            return value;
                        } else {
                            this.innerValue = value.id;
                            return this.getDisplay(value);
                        }
                    }),
                    filter(val => val.length > 2 || val.length === 0),
                    debounceTime(500),
                    mergeMap(val => {
                        if (val.length !== 0) {
                            return this.roleService.getRoles({
                                filter: { text: val }
                            }).pipe(map(res => res.pageItems));
                        } else {
                            return of(<Role[]>[]);
                        }
                    }));
        }
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

    getDisplay(role: Role) {
        return role ? role.name : '';
    }
}
