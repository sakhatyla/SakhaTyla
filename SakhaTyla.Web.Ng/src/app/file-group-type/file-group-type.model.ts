export enum FileGroupType {
  Database = 0,
  Storage = 1
}

const FileGroupTypeDisplay: { [index: number]: string } = {};
FileGroupTypeDisplay[FileGroupType.Database] = 'Database';
FileGroupTypeDisplay[FileGroupType.Storage] = 'Storage';
export { FileGroupTypeDisplay };
