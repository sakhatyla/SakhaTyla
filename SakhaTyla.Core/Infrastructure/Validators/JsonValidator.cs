using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;

namespace SakhaTyla.Core.Infrastructure.Validators
{
    public static class JsonValidator
    {
        public static IRuleBuilderOptions<T, string> IsJson<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(str => IsJson(str)).WithMessage("{PropertyName} is not valid JSON");
        }

        private static bool IsJson(string str)
        {
            if (string.IsNullOrEmpty(str))
                return true;
            try
            {
                JsonDocument.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
