import { Directive, EventEmitter, HostListener, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { MatCheckbox } from '@angular/material/checkbox';

@Directive({
  selector: '[appSelectAllItems]'
})
export class SelectAllItemsDirective implements OnChanges {

  @Input()
  appSelectAllItems: { id: number }[];

  @Input()
  selectedIds: Set<number>;

  @Output()
  selectedIdsChange = new EventEmitter<Set<number>>();

  constructor(private matCheckbox: MatCheckbox) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.selectedIds) {
      if (this.appSelectAllItems) {
        this.matCheckbox.checked = this.allItemsSelected();
      }
    }
  }

  private allItemsSelected(): boolean {
    for (const item of this.appSelectAllItems) {
      if (!this.selectedIds.has(item.id)) {
        return false;
      }
    }
    return this.appSelectAllItems.length > 0;
  }

  @HostListener('change', ['$event.checked'])
  public onChange(checked: boolean) {
    for (const item of this.appSelectAllItems) {
      if (checked) {
        this.selectedIds.add(item.id);
      } else {
        this.selectedIds.delete(item.id);
      }
    }
    this.selectedIds = new Set<number>([...this.selectedIds]);
    this.selectedIdsChange.emit(this.selectedIds);
  }
}
