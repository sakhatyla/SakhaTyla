using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SakhaTyla.Core.Workers
{
    public class WorkerContext
    {
        public WorkerContext(string data, CancellationToken cancellationToken)
        {
            Data = data;
            CancellationToken = cancellationToken;
        }

        public CancellationToken CancellationToken { get; }

        public string Data { get; }

        public string Result { get; set; }

        public string ResultData { get; set; }
    }
}
