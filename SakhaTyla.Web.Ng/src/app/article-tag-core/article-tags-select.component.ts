import { Component, Input, OnInit, OnDestroy, ElementRef, Optional, Self, DoCheck } from '@angular/core';
import { ControlValueAccessor, NgControl } from '@angular/forms';
import { MatFormFieldControl } from '@angular/material/form-field';
import { FocusMonitor } from '@angular/cdk/a11y';
import { coerceBooleanProperty } from '@angular/cdk/coercion';
import { Subject } from 'rxjs';

import { Tag } from '../tag-core/tag.model';
import { TagService } from '../tag-core/tag.service';

@Component({
  selector: 'app-article-tags-select',
  templateUrl: './article-tags-select.component.html',
  providers: [
    { provide: MatFormFieldControl, useExisting: ArticleTagsSelectComponent }
  ]
})

export class ArticleTagsSelectComponent implements OnInit, ControlValueAccessor,
  MatFormFieldControl<number[] | null>, OnDestroy, DoCheck {

  static nextId = 0;

  stateChanges = new Subject<void>();
  focused = false;
  controlType = 'app-article-tags-select';
  id = `article-tags-select-${ArticleTagsSelectComponent.nextId++}`;
  describedBy = '';

  errorState = false;

  get empty() {
    return !this.value;
  }

  get shouldLabelFloat() { return this.focused || !this.empty; }

  tags: Tag[] = [];

  @Input()
  value: number[] | null = null;

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

  constructor(private tagService: TagService,
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
  }

  ngOnInit(): void {
    this.tagService.getTags({}).subscribe(tags => this.tags = tags.pageItems);
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

  getSelectedTags() {
    return this.tags.filter(e => this.innerValue?.includes(e.id)).map(e => e.name).join(', ');
  }
}
