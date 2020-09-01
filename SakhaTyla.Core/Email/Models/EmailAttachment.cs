using System.IO;
using System.Threading.Tasks;

namespace SakhaTyla.Core.Email.Models
{
    public class EmailAttachment
    {
        public EmailAttachment()
        {
        }

        public EmailAttachment(byte[] data, string fileName)
        {
            Data = data;
            FileName = fileName;
        }

        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public static async Task<EmailAttachment> FromStreamAsync(Stream stream, string fileName)
        {
            using (var memo = new MemoryStream())
            {
                await stream.CopyToAsync(memo);
                return new EmailAttachment(memo.GetBuffer(), fileName);
            }
        }
    }
}
