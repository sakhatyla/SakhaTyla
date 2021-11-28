import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ConvertStringTo } from '../core/converter.helper';

import { FileGroup } from '../file-group-core/file-group.model';
import { FileGroupService } from '../file-group-core/file-group.service';
import { FileGroupEditComponent } from './file-group-edit.component';

@Component({
  selector: 'app-file-group-view',
  templateUrl: './file-group-view.component.html',
  styleUrls: ['./file-group-view.component.scss']
})
export class FileGroupViewComponent implements OnInit {
  id: number;
  fileGroup: FileGroup;

  constructor(private dialog: MatDialog,
              private fileGroupService: FileGroupService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.params.forEach((params: Params) => {
      this.id = ConvertStringTo.number(params.id);
      this.getFileGroup();
    });
  }

  private getFileGroup() {
    this.fileGroupService.getFileGroup({ id: this.id })
      .subscribe(fileGroup => this.fileGroup = fileGroup);
  }

  onEdit() {
    FileGroupEditComponent.show(this.dialog, this.id).subscribe(() => {
      this.getFileGroup();
    });
  }

  onBack(): void {
    window.history.back();
  }
}
