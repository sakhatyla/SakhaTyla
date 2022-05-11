using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Cynosura.Core.Services.Models;

namespace SakhaTyla.Core.FileStorage
{
    public class FileSystemStorage : IFileStorage
    {
        private readonly FileSystemStorageSettings _settings;

        public FileSystemStorage(IOptions<FileSystemStorageSettings> settings)
        {
            _settings = settings.Value;
        }

        private string GetRootPath()
        {
            if (_settings.Path == null)
            {
                throw new Exception("Path settings is empty");
            }
            return _settings.Path;
        }

        private void ValidatePath(string path, bool excludeRoot = false)
        {
            var relativePath = Path.GetRelativePath(GetRootPath(), path);
            if (relativePath.StartsWith(".."))
            {
                throw new Exception("Invalid path");
            }
            if (excludeRoot)
            {
                if (relativePath == "" || relativePath == "/" || relativePath == ".")
                {
                    throw new Exception("Invalid path");
                }
            }
        }

        private string GetFilePathFromUrl(string url)
        {
            var filePath = url.Replace($"{_settings.Url}/", "");
            return Path.Combine(GetRootPath(), filePath);
        }

        public Task DeleteFileAsync(string url)
        {
            var absoluteFilePath = GetFilePathFromUrl(url);
            ValidatePath(absoluteFilePath);
            File.Delete(absoluteFilePath);
            return Task.CompletedTask;
        }

        public async Task<byte[]> DownloadFileAsync(string url)
        {
            var absoluteFilePath = GetFilePathFromUrl(url);
            ValidatePath(absoluteFilePath);
            return await File.ReadAllBytesAsync(absoluteFilePath);
        }

        public Task<Stream> GetFileStreamAsync(string url)
        {
            var absoluteFilePath = GetFilePathFromUrl(url);
            ValidatePath(absoluteFilePath);
            var fs = new FileStream(absoluteFilePath, FileMode.Open);
            return Task.FromResult<Stream>(fs);
        }

        public Task<Stream> GetWriteFileStreamAsync(string filePath, string contentType)
        {
            var absoluteFilePath = Path.Combine(GetRootPath(), filePath);
            ValidatePath(absoluteFilePath);
            var fs = new FileStream(absoluteFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            return Task.FromResult<Stream>(fs);
        }

        public Task<PageModel<Entry>> GetEntriesAsync(string path, int pageIndex, int pageSize)
        {
            var absolutePath = Path.Combine(GetRootPath(), path);
            ValidatePath(absolutePath);
            var entries = Directory.GetFileSystemEntries(absolutePath);
            var pageEntries = entries
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(e => GetEntry(e))
                .ToList();
            return Task.FromResult(new PageModel<Entry>(pageEntries, entries.Length, pageIndex));
        }

        private Entry GetEntry(string path)
        {
            var relativePath = Path.GetRelativePath(GetRootPath(), path);
            var url = $"{_settings.Url}/{relativePath.Replace("\\", "/")}";
            var name = Path.GetFileName(relativePath);
            var type = GetEntryType(path);
            DateTime? creationDate;
            DateTime? modificationDate;
            if (type == EntryType.File)
            {
                var fileInfo = new FileInfo(path);
                creationDate = fileInfo.CreationTimeUtc;
                modificationDate = fileInfo.LastWriteTimeUtc;
            }
            else
            {
                var directoryInfo = new DirectoryInfo(path);
                creationDate = directoryInfo.CreationTimeUtc;
                modificationDate = directoryInfo.LastWriteTimeUtc;
            }

            return new Entry(name, url, type, creationDate: creationDate, modificationDate: modificationDate);
        }

        private static EntryType GetEntryType(string path)
        {
            var attr = File.GetAttributes(path);
            return attr.HasFlag(FileAttributes.Directory) ? EntryType.Directory : EntryType.File;
        }

        public async Task<string> SaveFileAsync(string filePath, Stream content, string contentType)
        {
            var absoluteFilePath = Path.Combine(GetRootPath(), filePath);
            ValidatePath(absoluteFilePath);
            var directory = Path.GetDirectoryName(absoluteFilePath)!;
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

        public Task<string> CreateDirectoryAsync(string directoryPath)
        {
            var absoluteDirectoryPath = Path.Combine(GetRootPath(), directoryPath);
            ValidatePath(absoluteDirectoryPath);
            if (!Directory.Exists(absoluteDirectoryPath))
            {
                Directory.CreateDirectory(absoluteDirectoryPath);
            }
            return Task.FromResult($"{_settings.Url}/{directoryPath}");
        }

        public Task DeleteDirectoryAsync(string url)
        {
            var absoluteDirectoryPath = GetFilePathFromUrl(url);
            ValidatePath(absoluteDirectoryPath, true);
            Directory.Delete(absoluteDirectoryPath, true);
            return Task.CompletedTask;
        }
    }
}
