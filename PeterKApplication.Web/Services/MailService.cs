using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using PeterKApplication.Shared.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace PeterKApplication.Web.Services
{
    public interface IMailService
    {
        bool SendMail(AppUser receiver, string subject, string message);
    }

    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendMail(AppUser receiver, string subject, string message)
        {
            Console.WriteLine("sending email with subject:" + subject);
            Console.WriteLine("sending email and message :" + message);
            var client = new SmtpClient("mail.seampos.com")
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("noreply@seampos.com", "1234pos@#")
            };

            var mailMessage = new MailMessage {From = new MailAddress("noreply@seampos.com")};
            mailMessage.To.Add(receiver.Email);
            mailMessage.Body = message;
            mailMessage.Subject = subject;
            client.Send(mailMessage);

            return true;
        }
    }
}