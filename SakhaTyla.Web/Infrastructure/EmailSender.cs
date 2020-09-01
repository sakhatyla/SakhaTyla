using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using SakhaTyla.Core.Email.Models;

namespace SakhaTyla.Web.Infrastructure
{
    public class EmailSender : IEmailSender
    {
        private readonly Core.Email.IEmailSender _emailSender;

        public EmailSender(Core.Email.IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return _emailSender.SendAsync(new EmailModel()
            {
                To = email,
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            });
        }
    }
}
