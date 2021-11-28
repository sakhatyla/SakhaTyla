using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Cynosura.Core.Services.Models;

namespace SakhaTyla.Core.FileStorage
{
    public interface IFileStorage
    {
        Task<string> SaveFileAsync(string filePath, Stream content, string contentType);
        Task DeleteFileAsync(string url);
        Task<byte[]> DownloadFileAsync(string url);
        Task<Stream> GetFileStreamAsync(string url);
        Task<Stream> GetWriteFileStreamAsync(string filePath, string contentType);
        Task<PageModel<Entry>> GetEntriesAsync(string path, int pageIndex, int pageSize);
        Task<string> CreateDirectoryAsync(string directoryPath);
        Task DeleteDirectoryAsync(string url);
    }
}
