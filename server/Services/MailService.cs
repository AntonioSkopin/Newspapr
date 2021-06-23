using System;
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

        // Can't connect yet ***NOT WORKING
        public void SendRegisterConfirmationMail(string userMail, string pincode)
        {
            // Get ENV variables
            var variablesSection = _configuration.GetSection("Variables");
            var envVariables = variablesSection.Get<EnvVariables>();

            string toPerson = userMail;
            string from = envVariables.MAIL_USERNAME;

            MailMessage message = new MailMessage(from, toPerson);
            message.Subject = "Please verify your account!";
            message.IsBodyHtml = true;
            message.Body = "<h3>Your unique PIN code to activate your account: " + pincode + ".</h3><p>Activate your account by clicking on this <a href='https://localhost:5001/Auth/ActivateAccount'>link</a>!</p>";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new System.Net.NetworkCredential()
            {
                UserName = envVariables.MAIL_USERNAME,
                Password = envVariables.MAIL_PASSWORD
            };
            try
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in SendRegisterConfirmationMail: {0}", ex.ToString());
            }
        }
    }
}