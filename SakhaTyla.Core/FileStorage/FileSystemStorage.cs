using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace SakhaTyla.Core.FileStorage
{
    public class FileSystemStorage : IFileStorage
    {
        private readonly FileSystemStorageSettings _settings;

        public FileSystemStorage(IOptions<FileSystemStorageSettings> settings)
        {
            _settings = settings.Value;
        }

        public Task DeleteFileAsync(string url)
        {
            var filePath = url.Replace($"{_settings.Url}/", "");
            var absoluteFilePath = Path.Combine(_settings.Path, filePath);
            File.Delete(absoluteFilePath);
            return Task.CompletedTask;
        }

        public async Task<byte[]> DownloadFileAsync(string url)
        {
            var filePath = url.Replace($"{_settings.Url}/", "");
            var absoluteFilePath = Path.Combine(_settings.Path, filePath);
            return await File.ReadAllBytesAsync(absoluteFilePath);
        }

        public async Task<string> SaveFileAsync(string filePath, Stream content, string contentType)
        {
            var absoluteFilePath = Path.Combine(_settings.Path, filePath);
            var directory = Path.GetDirectoryName(absoluteFilePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            using (var fileStream = File.Create(absoluteFilePath))
            {
                await content.CopyToAsync(fileStream);
            }
            return $"{_settings.Url}/{filePath}";
        }
    }
}
