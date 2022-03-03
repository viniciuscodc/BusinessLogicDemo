using System;
using System.Net;
using System.Net.Mail;

namespace Application.Services.EmailService
{
    public abstract class SMTPProvider
    {
        public string Send(MailMessage mailMessage, NetworkCredential credentials)
        {
            using (var smtpClient = new SmtpClient("email-ssl.com.br", 587))
            {
                try
                {
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());

                    return "Fail";
                }
            }

            return "Success";
        }
    }
}
