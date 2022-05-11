import { Directive, EventEmitter, HostListener, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { MatCheckbox } from '@angular/material/checkbox';

@Directive({
  selector: '[appSelectItem]'
})
export class SelectItemDirective implements OnChanges {

  @Input()
  appSelectItem: { id: number };

  @Input()
  selectedIds: Set<number>;

  @Output()
  selectedIdsChange = new EventEmitter<Set<number>>();

  constructor(private matCheckbox: MatCheckbox) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.selectedIds) {
      if (this.appSelectItem) {
        const selected = this.selectedIds.has(this.appSelectItem.id);
        this.matCheckbox.checked = selected;
      }
    }
  }

  @HostListener('click', ['$event'])
  public onClick($event: Event) {
    $event.stopPropagation();
  }

  @HostListener('change', ['$event.checked'])
  public onChange(checked: boolean) {
    if (checked) {
      this.selectedIds.add(this.appSelectItem.id);
    } else {
      this.selectedIds.delete(this.appSelectItem.id);
    }
    this.selectedIds = new Set<number>([...this.selectedIds]);
    this.selectedIdsChange.emit(this.selectedIds);
  }
}
