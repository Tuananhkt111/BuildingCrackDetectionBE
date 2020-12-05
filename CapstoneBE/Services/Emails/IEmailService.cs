using CapstoneBE.Models;

namespace CapstoneBE.Services.Emails
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}