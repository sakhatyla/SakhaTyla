using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using SakhaTyla.Core.Security;

namespace SakhaTyla.Web.Infrastructure
{
    public class UserInfoModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(UserInfoModel))
            {
                return new BinderTypeModelBinder(typeof(UserInfoModelBinder));
            }

            return null;
        }
    }
}
