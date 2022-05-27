import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { Tag } from '../tag-core/tag.model';
import { TagService } from '../tag-core/tag.service';
import { TagEditComponent } from './tag-edit.component';

@Component({
  selector: 'app-tag-view',
  templateUrl: './tag-view.component.html',
  styleUrls: ['./tag-view.component.scss']
})
export class TagViewComponent implements OnInit {
  id: number;
  tag: Tag;

  constructor(private dialog: MatDialog,
              private tagService: TagService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getTag();
    });
  }

  private getTag() {
    this.tagService.getTag({ id: this.id })
      .subscribe(tag => this.tag = tag);
  }

  onEdit() {
    TagEditComponent.show(this.dialog, this.id).subscribe(() => {
      this.getTag();
    });
  }

  onBack(): void {
    window.history.back();
  }
}
