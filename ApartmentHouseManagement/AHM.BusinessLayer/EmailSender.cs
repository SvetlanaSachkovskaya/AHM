using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using AHM.BusinessLayer.Interfaces;

namespace AHM.BusinessLayer
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _client;
        private readonly string _fromEmail;

        public EmailSender()
        {
            _fromEmail = ConfigurationManager.AppSettings["Email"];
            var username = ConfigurationManager.AppSettings["Username"];
            var password = ConfigurationManager.AppSettings["Password"];

            _client = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Timeout = 10000,
                Credentials = new NetworkCredential(username, password)
            };
        }

        public void Send(string toEmail, string subject, string message, string filePath = "")
        {
            var mailMessage = new MailMessage(_fromEmail, toEmail) { Subject = subject, Body = message };
            if (!String.IsNullOrEmpty(filePath))
            {
                mailMessage.Attachments.Add(new Attachment(filePath));
            }

            _client.Send(mailMessage);

            mailMessage.Dispose();
        }
    }
}