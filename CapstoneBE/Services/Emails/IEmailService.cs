using CapstoneBE.Models;
using System.Threading.Tasks;

namespace CapstoneBE.Services.Emails
{
    public interface IEmailService
    {
        void SendEmail(Email email);

        Task SendEmailAsync(Email email);
    }
}