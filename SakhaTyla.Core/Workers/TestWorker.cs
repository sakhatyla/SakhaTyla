using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SakhaTyla.Core.Workers
{
    public class TestWorker : IWorker
    {
        private readonly ILogger<TestWorker> _logger;

        public TestWorker(ILogger<TestWorker> logger)
        {
            _logger = logger;
        }

        public async Task ExecuteAsync(WorkerContext workerContext)
        {
            var data = workerContext.Data != null ? JsonSerializer.Deserialize<TestData>(workerContext.Data) : null;
            _logger.LogInformation("Running");
            await Task.Delay(TimeSpan.FromMinutes(1), workerContext.CancellationToken);
            workerContext.Result = $"Task completed: {data?.Message}";
        }

        class TestData
        {
            public string? Message { get; set; }
        }
    }
}
