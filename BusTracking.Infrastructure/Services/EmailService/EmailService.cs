using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusTracking.Infrastructure.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public async Task Send(string to, string subject, string body)
        {
            // create message
            var message = new MailMessage();

            message.From = new MailAddress("bustracking4563069@gmail.com", "Bus Tracking App");
            message.Subject = subject;
            message.Body = body;
            message.To.Add(new MailAddress(to));
            message.IsBodyHtml = false;


            try
            {
                var emailClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("bustracking4563069@gmail.com", "nuysnjydkvhugovs")
                };


                await emailClient.SendMailAsync(message);
            }
            catch (Exception e)
            {

            }
            // send email

        }

    }
}
