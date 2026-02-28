using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.Infrastructure
{
    public interface IDataContext
    {
        void ClearChangeTracker();
    }
}
