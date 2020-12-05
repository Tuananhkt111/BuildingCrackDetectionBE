using CapstoneBE.Helpers;
using CapstoneBE.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

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

        public async Task SendEmailAsync(Email email)
        {
            MimeMessage emailMessage = CreateEmailMessage(email);
            await SendAsync(emailMessage);
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

        private async Task SendAsync(MimeMessage emailMessage)
        {
            using SmtpClient client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);
                await client.SendAsync(emailMessage);
            }
            catch (Exception)
            {
                throw new Exception("Send email failed");
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}