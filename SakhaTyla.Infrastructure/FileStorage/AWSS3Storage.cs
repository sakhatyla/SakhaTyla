using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Cynosura.Core.Services.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using SakhaTyla.Core.FileStorage;

namespace SakhaTyla.Infrastructure.FileStorage
{
    public class AWSS3Storage : IFileStorage
    {
        private readonly AWSS3StorageSettings _settings;

        public AWSS3Storage(IOptions<AWSS3StorageSettings> settings)
        {
            _settings = settings.Value;
        }

        private AmazonS3Client GetClient()
        {
            return new AmazonS3Client(_settings.AccessKey, _settings.SecretKey, new AmazonS3Config
            {
                ServiceURL = _settings.ServiceUrl
            });
        }

        public async Task<string> CreateDirectoryAsync(string directoryPath)
        {
            var client = GetClient();
            var request = new PutObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = directoryPath + "/"
            };
            await client.PutObjectAsync(request);
            return $"{_settings.Url}/{directoryPath}";
        }

        public async Task DeleteDirectoryAsync(string url)
        {
            var path = GetFilePathFromUrl(url);
            await DeleteFiles(path);
        }

        private async Task DeleteFiles(string path)
        {
            var s3Objects = ListObjects(path, true);
            var keys = new List<KeyVersion>();

            await foreach (var s3Object in s3Objects)
            {
                if (s3Object.CommonPrefix != null)
                {
                    await DeleteFiles($"{s3Object.CommonPrefix}");
                }
                else
                {
                    keys.Add(new KeyVersion() { Key = s3Object.S3Object!.Key });
                }
            }
            var client = GetClient();
            var request = new DeleteObjectsRequest()
            {
                BucketName = _settings.BucketName,
                Objects = keys
            };
            await client.DeleteObjectsAsync(request);
        }

        public async Task DeleteFileAsync(string url)
        {
            var client = GetClient();
            var request = new DeleteObjectRequest()
            {
                BucketName = _settings.BucketName,
                Key = GetFilePathFromUrl(url)
            };
            await client.DeleteObjectAsync(request);
        }

        public async Task<byte[]> DownloadFileAsync(string url)
        {
            var client = GetClient();
            var request = new GetObjectRequest()
            {
                BucketName = _settings.BucketName,
                Key = GetFilePathFromUrl(url)
            };
            var response = await client.GetObjectAsync(request);
            return ConvertToBytes(response.ResponseStream);
        }

        private async IAsyncEnumerable<S3ObjectOrCommonPrefix> ListObjects(string path, bool includeRoot = false)
        {
            var client = GetClient();
            if (path != "" && !path.EndsWith("/"))
            {
                path += "/";
            }
            string? nextContinuationToken = null;
            while (true)
            {
                var request = new ListObjectsV2Request()
                {
                    BucketName = _settings.BucketName,
                    Delimiter = "/",
                    Prefix = path,
                    ContinuationToken = nextContinuationToken,
                };
                var response = await client.ListObjectsV2Async(request);
                foreach (var commonPrefix in response.CommonPrefixes)
                {
                    yield return new S3ObjectOrCommonPrefix(commonPrefix);
                }
                foreach (var s3Object in response.S3Objects)
                {
                    if (s3Object.Key != path || includeRoot)
                    {
                        yield return new S3ObjectOrCommonPrefix(s3Object);
                    }
                }
                if (!response.IsTruncated)
                {
                    break;
                }
                nextContinuationToken = response.NextContinuationToken;
            }
        }

        public async Task<PageModel<Entry>> GetEntriesAsync(string path, int pageIndex, int pageSize)
        {
            var objects = await ListObjects(path)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var entries = objects.Select(o => o.S3Object != null ? GetEntry(o.S3Object) : GetEntry(o.CommonPrefix!))
                .ToList();
            var total = await ListObjects(path).CountAsync();
            return new PageModel<Entry>(entries, total, pageIndex);
        }

        public async Task<Stream> GetFileStreamAsync(string url)
        {
            var client = GetClient();
            var request = new GetObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = GetFilePathFromUrl(url)
            };
            var response = await client.GetObjectAsync(request);
            return response.ResponseStream;
        }

        public Task<Stream> GetWriteFileStreamAsync(string filePath, string contentType)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveFileAsync(string filePath, Stream content, string contentType)
        {
            var client = GetClient();
            var request = new PutObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = filePath,
                InputStream = content,
                AutoCloseStream = false,
                ContentType = contentType
            };
            await client.PutObjectAsync(request);
            return $"{_settings.Url}/{filePath}";
        }

        private Entry GetEntry(S3Object s3Object)
        {
            var fileName = GetFileName(s3Object.Key);
            var url = $"{_settings.Url}/{s3Object.Key}";
            return new Entry(fileName, url, EntryType.File, MimeTypes.GetMimeType(fileName), null, s3Object.LastModified);
        }

        private Entry GetEntry(string directory)
        {
            var url = $"{_settings.Url}/{directory}";
            return new Entry(GetDirectoryName(directory), url, EntryType.Directory);
        }

        public string GetFilePathFromUrl(string url)
        {
            return url.Replace($"{_settings.Url}/", "");
        }

        private static string GetFileName(string path)
        {
            return path.Split('/').Last();
        }

        private static string GetDirectoryName(string path)
        {
            return path.Split('/').Reverse().Skip(1).First();
        }

        private static byte[] ConvertToBytes(Stream input)
        {
            using var ms = new MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }

        private class S3ObjectOrCommonPrefix
        {
            public S3ObjectOrCommonPrefix(S3Object s3Object)
            {
                S3Object = s3Object;
            }

            public S3ObjectOrCommonPrefix(string commonPrefix)
            {
                CommonPrefix = commonPrefix;
            }

            public S3Object? S3Object { get; set; }
            public string? CommonPrefix { get; set; }
        }
    }
}
