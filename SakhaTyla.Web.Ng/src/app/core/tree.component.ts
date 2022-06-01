import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';

import { FlatTreeControl } from '@angular/cdk/tree';
import { Observable, of as observableOf } from 'rxjs';
import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { MatTreeFlatDataSource, MatTreeFlattener } from '@angular/material/tree';

export class FlatNode {
    constructor(
        public expandable: boolean,
        public name: string,
        public level: number,
        public weight: number,
        public id: number,
        public parentId: number,
        public hideCheckbox?: boolean
    ) { }
}

@Component({
    selector: 'app-tree',
    templateUrl: 'tree.component.html',
    styleUrls: ['tree.component.scss'],
})
export class TreeComponent implements OnInit {
    @Input()
    expandLevel: number;

    @Input()
    selectedIds: number[];

    @Input()
    canEdit = true;

    @Input()
    checkHideCheckbox = false;

    @Input()
    set data(value: []) {
        this.dataSource.data = value;
        this.expand();
    }

    @Output()
    public orderedData = new EventEmitter();

    @Output()
    public edit = new EventEmitter();

    @Output()
    public delete = new EventEmitter();

    treeControl: FlatTreeControl<FlatNode>;
    treeFlattener: MatTreeFlattener<any, FlatNode>;
    dataSource: MatTreeFlatDataSource<any, FlatNode>;
    expandedNodeSet = new Set<number>();
    dragging = false;
    expandTimeout: any;
    expandDelay = 1000;
    transformer = (node: any, level: number) => {
        return new FlatNode(node.children && node.children.length > 0, node.name, level, node.weight, node.id,
            node.parentId, node.hideCheckbox);
    }
    private getLevel = (node: FlatNode) => node.level;
    private isExpandable = (node: FlatNode) => node.expandable;
    private getChildren = (node: any): Observable<any[]> => observableOf(node.children);
    hasChild = (_: number, nodeData: FlatNode) => nodeData.expandable;

    constructor() {
        this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel,
            this.isExpandable, this.getChildren);
        this.treeControl = new FlatTreeControl<FlatNode>(this.getLevel, this.isExpandable);
        this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
    }

    ngOnInit(): void {
        if (this.selectedIds && this.selectedIds.length > 0) {
            this.treeControl.dataNodes.forEach(node => {
                this.setCheckboxes(node);
            });
        }
    }

    setCheckboxes(node) {
        const existId = this.selectedIds.find(id => id === node.id);
        if (existId) {
            setTimeout(() => node.checked = true);
        }
        if (node.children) {
            node.children.forEach(child => {
                this.setCheckboxes(child);
            });
        }
    }

    expand() {
        if (this.expandedNodeSet !== undefined && this.expandedNodeSet.size > 0) {
            this.expandNodesById(this.treeControl.dataNodes, Array.from(this.expandedNodeSet));
        } else {
            const expandIds = [];
            if (this.expandLevel !== undefined) {
                this.treeControl.dataNodes.forEach((node) => {
                    if (node.level === this.expandLevel) {
                        expandIds.push(node.id);
                    }
                });
                expandIds.forEach((id) => {
                    this.expandedNodeSet.add(id);
                });
            } else {
                this.treeControl.dataNodes.forEach((node) => {
                    expandIds.push(node.id);
                    if (node.expandable) {
                        this.expandedNodeSet.add(node.id);
                    }
                });
            }
            this.expandNodesById(this.treeControl.dataNodes, expandIds);
        }
    }

    editNode(e: any, node): void {
        e.stopPropagation();
        if (!this.canEdit && this.selectedIds) {
            node.checked = !node.checked;
        } else {
            this.edit.emit(node.id);
        }
    }

    deleteNode(e: any, id: number): void {
        e.stopPropagation();
        this.delete.emit(id);
    }

    drop(event: CdkDragDrop<string[]>) {
        if (!event.isPointerOverContainer) {
            return;
        }
        const visibleNodes = this.visibleNodes();
        const changedData = JSON.parse(JSON.stringify(this.dataSource.data));
        const node = event.item.data;
        const siblings = this.findNodeSiblings(changedData, node.id);
        const siblingIndex = siblings.findIndex(n => n.id === node.id);
        const nodeToInsert: any = siblings.splice(siblingIndex, 1)[0];
        const nodeAtDest = visibleNodes[event.currentIndex];
        if (nodeAtDest.id === nodeToInsert.id || nodeAtDest.path !== nodeToInsert.path) {
            return;
        }
        let relativeIndex = event.currentIndex;
        const nodeAtDestFlatNode = this.treeControl.dataNodes.find(n => nodeAtDest.id === n.id);
        const parent = this.getParentNode(nodeAtDestFlatNode);
        if (parent) {
            const parentIndex = visibleNodes.findIndex(n => n.id === parent.id) + 1;
            relativeIndex = event.currentIndex - parentIndex;
        }
        const newSiblings = this.findNodeSiblings(changedData, nodeAtDest.id);
        if (!newSiblings) {
            return;
        }
        newSiblings.splice(relativeIndex, 0, nodeToInsert);
        this.rebuildTreeForData(changedData, newSiblings);
    }

    findNodeSiblings(arr: Array<any>, id: number): Array<any> {
        let result: any[];
        let subResult: any[];
        arr.forEach(item => {
            if (item.id === id) {
                result = arr;
            } else if (item.children) {
                subResult = this.findNodeSiblings(item.children, id);
                if (subResult) {
                    result = subResult;
                }
            }
        });
        return result;
    }

    dragStart() {
        this.dragging = true;
    }

    dragEnd() {
        this.dragging = false;
    }

    dragHover(node: FlatNode) {
        if (this.dragging) {
            clearTimeout(this.expandTimeout);
            this.expandTimeout = setTimeout(() => {
                this.treeControl.expand(node);
            }, this.expandDelay);
        }
    }

    dragHoverEnd() {
        if (this.dragging) {
            clearTimeout(this.expandTimeout);
        }
    }


    expandedNodeState(node: FlatNode) {
        if (this.expandedNodeSet.has(node.id)) {
            this.expandedNodeSet.delete(node.id);
        } else {
            this.expandedNodeSet.add(node.id);
        }
    }

    visibleNodes(): any[] {
        this.rememberExpandedTreeNodes(this.treeControl, this.expandedNodeSet);
        const result = [];

        function addExpandedChildren(node: any, expanded: Set<number>) {
            result.push(node);
            if (expanded.has(node.id)) {
                node.children.map(child => addExpandedChildren(child, expanded));
            }
        }
        this.dataSource.data.forEach(node => {
            addExpandedChildren(node, this.expandedNodeSet);
        });
        return result;
    }

    rebuildTreeForData(data: any[], newSiblings: any[]) {
        this.expandedNodeSet = new Set<number>();
        this.rememberExpandedTreeNodes(this.treeControl, this.expandedNodeSet);
        this.dataSource.data = data;
        this.expandNodesById(this.treeControl.dataNodes, Array.from(this.expandedNodeSet));
        // todo ?
        // this.forgetMissingExpandedNodes(this.treeControl, this.expandedNodeSet);
        if (newSiblings && newSiblings.length > 0) {
            let weight = newSiblings.length;
            const items = [];
            for (const item of newSiblings) {
                item.weight = weight--;
                items.push(item);
            }
            this.orderedData.emit(items);
        }
    }

    updateSelectedIds(checked: boolean, node) {
        if (checked) {
            this.selectedIds.push(node.id);
        } else {
            const index = this.selectedIds.indexOf(node.id);
            if (index > -1) {
                this.selectedIds.splice(index, 1);
            }
        }
    }

    private rememberExpandedTreeNodes(
        treeControl: FlatTreeControl<FlatNode>,
        expandedNodeSet: Set<number>
    ) {
        if (treeControl.dataNodes) {
            treeControl.dataNodes.forEach((node) => {
                if (treeControl.isExpandable(node) && treeControl.isExpanded(node)) {
                    expandedNodeSet.add(node.id);
                }
            });
        }
    }

    private expandNodesById(flatNodes: FlatNode[], ids: number[]) {
        if (!flatNodes || flatNodes.length === 0) {
            return;
        }
        const idSet = new Set(ids);
        return flatNodes.forEach((node) => {
            if (idSet.has(node.id)) {
                this.treeControl.expand(node);
                let parent = this.getParentNode(node);
                while (parent) {
                    this.treeControl.expand(parent);
                    parent = this.getParentNode(parent);
                }
            }
        });
    }

    private getParentNode(node: FlatNode): FlatNode | null {
        const currentLevel = node.level;
        if (currentLevel < 1) {
            return null;
        }
        const startIndex = this.treeControl.dataNodes.indexOf(node) - 1;
        for (let i = startIndex; i >= 0; i--) {
            const currentNode = this.treeControl.dataNodes[i];
            if (currentNode.level < currentLevel) {
                return currentNode;
            }
        }
        return null;
    }
}
