using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Formatters
{
    public static class ExcelFormatterExtensions
    {
        public static async Task<FileContentModel> GetExcelFileAsync<T>(this IExcelFormatter excelFormatter, IEnumerable<T> data, string filename)
        {
            using var ms = new MemoryStream();
            await excelFormatter.SaveToAsync(ms, data, true);
            return new FileContentModel()
            {
                Name = $"{filename}.xlsx",
                Content = ms.ToArray(),
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            };
        }
    }
}
