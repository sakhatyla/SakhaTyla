using System.Threading.Tasks;
using SakhaTyla.Core.Email.Models;

namespace SakhaTyla.Core.Email
{
    public interface IEmailSender
    {
        Task SendAsync(EmailModel model);
    }
}
