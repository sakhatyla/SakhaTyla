using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz.Logging;

namespace SakhaTyla.Worker.Infrastructure
{
    class CoreLogProvider : ILogProvider
    {
        private readonly ILoggerFactory _loggerFactory;

        public CoreLogProvider(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        private Microsoft.Extensions.Logging.LogLevel GetLevel(Quartz.Logging.LogLevel level)
        {
            switch (level)
            {
                case Quartz.Logging.LogLevel.Debug:
                    return Microsoft.Extensions.Logging.LogLevel.Debug;
                case Quartz.Logging.LogLevel.Error:
                    return Microsoft.Extensions.Logging.LogLevel.Error;
                case Quartz.Logging.LogLevel.Fatal:
                    return Microsoft.Extensions.Logging.LogLevel.Critical;
                case Quartz.Logging.LogLevel.Info:
                    return Microsoft.Extensions.Logging.LogLevel.Information;
                case Quartz.Logging.LogLevel.Trace:
                    return Microsoft.Extensions.Logging.LogLevel.Trace;
                case Quartz.Logging.LogLevel.Warn:
                    return Microsoft.Extensions.Logging.LogLevel.Warning;
            }
            throw new Exception("Level not found");
        }

        public Logger GetLogger(string name)
        {
            var logger = _loggerFactory.CreateLogger(name);
            return (level, func, exception, parameters) =>
            {
                if (func != null)
                {
                    logger.Log(GetLevel(level), exception, func(), parameters);
                }
                return true;
            };
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
