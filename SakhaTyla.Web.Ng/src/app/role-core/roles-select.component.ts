import { Component, Input, forwardRef, OnInit } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Role } from './role.model';
import { RoleService } from './role.service';

@Component({
  selector: 'app-roles-select',
  templateUrl: './roles-select.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => RolesSelectComponent),
      multi: true
    }
  ]
})
export class RolesSelectComponent implements ControlValueAccessor, OnInit {

  @Input()
  value: number[];

  @Input()
  name: string;

  @Input()
  label: string;

  @Input()
  disabled = false;

  roles: Role[] = [];

  get innerValue() {
    return this.value;
  }

  set innerValue(val) {
    this.value = val;
    this.onChange(this.value);
    this.onTouched();
  }

  onChange: any = () => { };
  onTouched: any = () => { };

  constructor(private roleService: RoleService) {}

  ngOnInit(): void {
    this.roleService.getRoles({})
      .subscribe(roles => {
        this.roles = roles.pageItems;
        this.setSelectedRoles();
      });
  }

  private setSelectedRoles() {
    for (const role of this.roles) {
      if (this.value && this.value.indexOf(role.id) !== -1) {
        role.isSelected = true;
      }
    }
  }

  roleSelectedChanged(role: Role, selected: boolean) {
    role.isSelected = selected;
    this.innerValue = this.roles
      .filter(r => r.isSelected)
      .map(r => r.id);
  }

  registerOnChange(fn) {
    this.onChange = fn;
  }

  registerOnTouched(fn) {
    this.onTouched = fn;
  }

  writeValue(value) {
    this.innerValue = value;
    this.setSelectedRoles();
  }

  setDisabledState(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
}
