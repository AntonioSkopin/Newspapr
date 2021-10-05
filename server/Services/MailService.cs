using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using server.Helpers;

namespace server.Services
{
    public class MailService
    {
        public IConfiguration _configuration { get; }

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendRegisterConfirmationMail(string userMail, string pincode)
        {
            // Get ENV variables
            var variablesSection = _configuration.GetSection("Variables");
            var envVariables = variablesSection.Get<EnvVariables>();

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(envVariables.MAIL_USERNAME.ToString());
            msg.To.Add("askopin@outlook.com");
            msg.Subject = "Please verify your account!";
            msg.IsBodyHtml = true;
            msg.Body = "<h3>Your unique PIN code to activate your account: " + pincode + ".</h3><p>Activate your account by clicking on this <a href='https://localhost:5001/Auth/ActivateAccount'>link</a>!</p>";
            //msg.Priority = MailPriority.High;

            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(envVariables.MAIL_USERNAME.ToString(), envVariables.MAIL_PASSWORD.ToString());
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(msg);
            }
        }
    }
}