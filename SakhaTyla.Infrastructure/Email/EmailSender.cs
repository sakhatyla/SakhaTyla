using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SakhaTyla.Core.Email;
using SakhaTyla.Core.Email.Models;

namespace SakhaTyla.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderOptions _options;

        public EmailSender(IOptions<EmailSenderOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendAsync(EmailModel model)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(!string.IsNullOrEmpty(model.From) ? model.From : _options.DefaultFrom));
            message.To.Add(MailboxAddress.Parse(model.To));
            message.Subject = model.Subject;
            var bodyBuilder = new BodyBuilder();
            if (model.IsBodyHtml)
            {
                bodyBuilder.HtmlBody = model.Body;
            }
            else
            {
                bodyBuilder.TextBody = model.Body;
            }
            if (model.Attachments != null)
            {
                foreach (var attachment in model.Attachments)
                {
                    bodyBuilder.Attachments.Add(attachment.FileName, attachment.Data, ContentType.Parse(attachment.ContentType));
                }
            }
            message.Body = bodyBuilder.ToMessageBody();
            if (!string.IsNullOrEmpty(model.Cc))
            {
                message.Cc.Add(MailboxAddress.Parse(model.Cc));
            }
            if (!string.IsNullOrEmpty(model.Bcc))
            {
                message.Bcc.Add(MailboxAddress.Parse(model.Bcc));
            }

            if (string.IsNullOrEmpty(_options.PickupFolder))
            {
                using var client = new SmtpClient();
                await client.ConnectAsync(_options.SmtpServer, _options.SmtpPort, _options.EnableSsl);
                if (!string.IsNullOrEmpty(_options.UserName))
                {
                    await client.AuthenticateAsync(_options.UserName, _options.Password);
                }
                await client.SendAsync(message);
            }
            else
            {
                if (!Directory.Exists(_options.PickupFolder))
                    Directory.CreateDirectory(_options.PickupFolder);
                await SaveToPickupDirectoryAsync(message, _options.PickupFolder);
            }
        }

        private async Task SaveToPickupDirectoryAsync(MimeMessage message, string pickupDirectory)
        {
            var path = Path.Combine(pickupDirectory, $"{Guid.NewGuid()}.eml");
            using var stream = new FileStream(path, FileMode.CreateNew);
            await message.WriteToAsync(stream);
        }
    }
}
