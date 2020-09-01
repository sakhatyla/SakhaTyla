export enum ChangeAction {
    Add = 0,
    Update = 1,
    Delete = 2
}

const ChangeActionDisplay: { [index: number]: string } = {};
ChangeActionDisplay[ChangeAction.Add] = 'Add';
ChangeActionDisplay[ChangeAction.Update] = 'Update';
ChangeActionDisplay[ChangeAction.Delete] = 'Delete';
export { ChangeActionDisplay };
