import { Component, OnInit, Input, TemplateRef, ContentChildren, QueryList } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

import { Error } from '../core/error.model';
import { Page, PageSettings } from '../core/page.model';
import { NoticeHelper } from '../core/notice.helper';

import { EntityChange, EntityChangeListState, EntityPropertyChange } from '../entity-change-core/entity-change.model';
import { EntityChangeService } from '../entity-change-core/entity-change.service';
import { EntityChangeValueDirective } from './entity-change-value.directive';

@Component({
  selector: 'app-entity-change-list',
  templateUrl: './entity-change-list.component.html',
  styleUrls: ['./entity-change-list.component.scss']
})
export class EntityChangeListComponent implements OnInit {
  content: Page<EntityChange>;
  pageSizeOptions = PageSettings.pageSizeOptions;
  get columns(): string[] {
    const columns = [
      'entityId',
      'action',
      'changes',
      'creationDate',
      'creationUser'
    ];
    return columns.filter(c => {
      if (c === 'entityId') {
        if (this.entityId) {
          return false;
        }
      }
      return true;
    });
  }

  @Input()
  state: EntityChangeListState;

  @Input()
  entityName: string;

  @Input()
  entityId: number;

  @ContentChildren(EntityChangeValueDirective)
  entityChangeValues: QueryList<EntityChangeValueDirective>;

  constructor(
    private entitychangeService: EntityChangeService,
    private noticeHelper: NoticeHelper
    ) {
  }

  entityChangeValueTemplate(name: string): TemplateRef<any> {
    const entityChangeValue = this.entityChangeValues.filter(ecv => ecv.appEntityChangeValueName === name)[0];
    return entityChangeValue && entityChangeValue.template;
  }

  entityChangeValueTemplateContext(value: any): object {
    return { $implicit: value };
  }

  filterEntityPropertyChanges(changes: EntityPropertyChange[]): EntityPropertyChange[] {
    return changes.filter(c => this.entityChangeValueTemplate(c.name));
  }

  ngOnInit() {
    this.getEntityChanges();
  }

  private getEntityChanges() {
    this.entitychangeService.getEntityChanges({
      entityName: this.entityName,
      entityId: this.entityId,
      pageIndex: this.state.pageIndex,
      pageSize: this.state.pageSize
    }).subscribe(content => this.content = content);
  }

  onSearch() {
    this.getEntityChanges();
  }

  onPage(page: PageEvent) {
    this.state.pageIndex = page.pageIndex;
    this.state.pageSize = page.pageSize;
    this.getEntityChanges();
  }

  onError(error: Error) {
    if (error) {
      this.noticeHelper.showError(error);
    }
  }
}
