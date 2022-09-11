using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Infrastructure.FileStorage
{
    public class AWSS3StorageSettings
    {
        public string? AccessKey { get; set; }
        public string? SecretKey { get; set; }
        public string? BucketName { get; set; }
        public string? ServiceUrl { get; set; }
        public string? Url { get; set; }
    }
}
