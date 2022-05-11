using System;
using MediatR;
using SakhaTyla.Core.Requests.WorkerInfos.Models;

namespace SakhaTyla.Core.Requests.WorkerInfos
{
    public class GetWorkerInfo : IRequest<WorkerInfoModel?>
    {
        public int Id { get; set; }
    }
}
