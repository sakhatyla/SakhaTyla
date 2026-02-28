using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.Formatters
{
    public interface IExcelFormatter
    {
        Task<IEnumerable<T>> LoadFromAsync<T>(Stream stream, bool withHeader) where T : new();
        Task SaveToAsync<T>(Stream stream, IEnumerable<T> data, bool withHeader);
    }
}
