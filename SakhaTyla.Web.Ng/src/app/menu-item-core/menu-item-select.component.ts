﻿import { Component, Input, OnInit, forwardRef, OnDestroy, ElementRef, Optional, Self, DoCheck } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NgControl, FormControl } from '@angular/forms';
import { MatFormFieldControl } from '@angular/material/form-field';
import { FocusMonitor } from '@angular/cdk/a11y';
import { coerceBooleanProperty } from '@angular/cdk/coercion';
import { Subject, Observable, of } from 'rxjs';
import { filter, debounceTime, mergeMap, map, startWith } from 'rxjs/operators';

import { MenuItem } from './menu-item.model';
import { MenuItemService } from './menu-item.service';

@Component({
  selector: 'app-menu-item-select',
  templateUrl: './menu-item-select.component.html',
  providers: [
    { provide: MatFormFieldControl, useExisting: MenuItemSelectComponent }
  ]
})

export class MenuItemSelectComponent implements OnInit, ControlValueAccessor,
  MatFormFieldControl<number | null>, OnDestroy, DoCheck {

  static nextId = 0;

  stateChanges = new Subject<void>();
  focused = false;
  controlType = 'app-menu-item-select';
  id = `menu-item-select-${MenuItemSelectComponent.nextId++}`;
  describedBy = '';

  errorState = false;

  get empty() {
    return !this.value;
  }

  get shouldLabelFloat() { return this.focused || !this.empty || this.autocompleteControl.value; }

  MenuItem = MenuItem;

  menuItems: MenuItem[] = [];

  autocompleteMenuItems: Observable<MenuItem[]>;

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

  constructor(private menuItemService: MenuItemService,
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
        this.menuItemService.getMenuItem({ id: this.value })
          .subscribe(menuItem => this.autocompleteControl.setValue(menuItem));
      } else {
        this.autocompleteControl.setValue('');
      }
    }
  }

  ngOnInit(): void {
    if (this.mode === 'select') {
      this.menuItemService.getMenuItems({}).subscribe(menuItems => this.menuItems = menuItems.pageItems);
    } else if (this.mode === 'autocomplete') {
      this.autocompleteMenuItems = this.autocompleteControl.valueChanges
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
              return this.menuItemService.getMenuItems({
                filter: { text: val }
              }).pipe(map(res => res.pageItems));
            } else {
              return of(<MenuItem[]>[]);
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

  getDisplay(menuItem: MenuItem) {
    return menuItem ? menuItem.name : '';
  }
}
