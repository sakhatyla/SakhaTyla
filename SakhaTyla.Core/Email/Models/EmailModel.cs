namespace SakhaTyla.Core.Email.Models
{
    public class EmailModel
    {
        public EmailModel()
        {
        }

        public EmailModel(string @from, string to, string subject, string body, bool isHtmlBody = false,
            string? cc = null, string? bcc = null,
            EmailAttachment[]? attachments = null)
        {
            From = @from;
            To = to;
            Subject = subject;
            Body = body;
            IsBodyHtml = isHtmlBody;
            Cc = cc;
            Bcc = bcc;
            Attachments = attachments;
        }

        public string? From { get; set; }
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public bool IsBodyHtml { get; set; }
        public string? Cc { get; set; }
        public string? Bcc { get; set; }
        public EmailAttachment[]? Attachments { get; set; }
    }
}
