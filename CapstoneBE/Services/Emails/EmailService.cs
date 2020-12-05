using CapstoneBE.Helpers;
using CapstoneBE.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace CapstoneBE.Services.Emails
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void SendEmail(Email email)
        {
            MimeMessage emailMessage = CreateEmailMessage(email);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Email email)
        {
            MimeMessage emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.From));
            emailMessage.To.AddRange(email.To);
            emailMessage.Subject = email.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = email.Content };
            return emailMessage;
        }

        private void Send(MimeMessage emailMessage)
        {
            using SmtpClient client = new SmtpClient();
            try
            {
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
                client.Send(emailMessage);
            }
            catch (Exception)
            {
                throw new Exception("Send email failed");
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}