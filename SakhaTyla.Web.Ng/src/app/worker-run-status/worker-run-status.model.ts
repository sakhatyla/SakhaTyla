export enum WorkerRunStatus {
  New = 0,
  Running = 1,
  Completed = 2,
  Error = 3
}

const WorkerRunStatusDisplay: { [index: number]: string } = {};
WorkerRunStatusDisplay[WorkerRunStatus.New] = 'New';
WorkerRunStatusDisplay[WorkerRunStatus.Running] = 'Running';
WorkerRunStatusDisplay[WorkerRunStatus.Completed] = 'Completed';
WorkerRunStatusDisplay[WorkerRunStatus.Error] = 'Error';
export { WorkerRunStatusDisplay };
