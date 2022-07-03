using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Web.Common.Infrastructure
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        public int Priority => 0;

        public bool CanHandleException(Exception exception)
        {
            return exception is ValidationException;
        }

        public ObjectResult HandleException(Exception exception)
        {
            var validationException = (ValidationException)exception;
            return new BadRequestObjectResult(new
            {
                Message = validationException.Message,
                ModelState = GetModelState(validationException.Failures),
            });
        }

        private Dictionary<string, object> GetModelState(IDictionary<string, string[]> failures)
        {
            var result = new Dictionary<string, object>();

            foreach (var key in failures.Keys)
            {
                var errors = failures[key].Select(e => new ModelStateError() { ErrorMessage = e });
                var state = new SimpleModelState()
                {
                    Errors = errors.ToList(),
                };
                var keys = key.Split(".").Select(k => ToCamelCase(k)).ToArray();

                SetValue(result, keys, state);
            }

            return result;
        }

        private string ToCamelCase(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        }

        private void SetValue(Dictionary<string, object> dic, string[] keys, object value)
        {
            if (keys.Length > 1)
            {
                Dictionary<string, object> innerDic;
                if (dic.ContainsKey(keys[0]))
                {
                    innerDic = (Dictionary<string, object>)dic[keys[0]];
                }
                else
                {
                    innerDic = new Dictionary<string, object>();
                    dic[keys[0]] = innerDic;
                }
                SetValue(innerDic, keys.Skip(1).ToArray(), value);
            }
            else
            {
                dic[keys[0]] = value;
            }
        }
    }
}
