using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace SakhaTyla.Core.Infrastructure.Validators
{
    public static class ClassNameValidator
    {
        public static IRuleBuilderOptions<T, string?> ClassNameImplementsInterface<T>(this IRuleBuilder<T, string?> ruleBuilder, Type interfaceType)
        {
            return ruleBuilder.Must(name => ValidateClassNameInterface(name, interfaceType)).WithMessage($"Class does not exist or is not inherited from the {interfaceType.Name} interface");
        }

        private static bool ValidateClassNameInterface(string? className, Type interfaceType)
        {
            if (string.IsNullOrEmpty(className))
            {
                return true;
            }
            var classType = Type.GetType(className);
            if (classType == null)
            {
                return false;
            }
            return classType.IsAssignableTo(interfaceType);
        }
    }
}
