using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.FileStorage
{
    public interface IFileStorage
    {
        Task<string> SaveFileAsync(string filePath, Stream content, string contentType);
        Task DeleteFileAsync(string url);
        Task<byte[]> DownloadFileAsync(string url);
    }
}
