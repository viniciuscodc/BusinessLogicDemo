using Application.Models.Configuration;
using Application.Models.RastreioSAC;
using Microsoft.Extensions.Options;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;

namespace Application.Services.EmailService
{
    public class EmailProvider : SMTPProvider, IEmailProvider
    {
        private readonly NetworkCredential Credentials;
        private readonly string SenderEmail;

        public EmailProvider(IOptions<Locaweb> config)
        {
            Credentials = new NetworkCredential(config.Value.Login, config.Value.Password);
            SenderEmail = config.Value.Login;
        }

        public string SendMeetingInvite(string subject, DateTime startTime, double duration, List<string> emails, string description)
        {
            var startTimeStr = startTime.ToString("dd/MM/yyyy HH:mm:ss");

            if (description == null)
            {
                description = "";
            }

            ProcessStartInfo start = new ProcessStartInfo();

            var projectPath = Directory.GetCurrentDirectory();
            string solutionPath;
            string pythonFilePath;

            if (OperatingSystem.IsLinux())
            {
                start.FileName = "/usr/bin/python3";
                solutionPath = projectPath;
                pythonFilePath = solutionPath + "/Services/EmailService/sendInvite.py";
            }
            else
            {
                //start.FileName = "C:/Users/organnact/AppData/Local/Programs/Python/Python39/python.exe";
                start.FileName = "C:/Users/organnact/AppData/Local/Programs/Python/Python39/python.exe";
                solutionPath = projectPath.Substring(0, projectPath.Length - 8);
                pythonFilePath = solutionPath + "Application/Services/EmailService/sendInvite.py";
            }

            start.Arguments = string.Format(
                pythonFilePath + " " + $"\"{startTimeStr}\" " + duration + $" \"{description}\"" + $" \"{subject}\"");

            foreach (var email in emails)
            {
                start.Arguments = start.Arguments + " " + email;
            }

            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.WriteLine(result);

                    return result.Trim();
                }
            }
        }

        public string SendEmail(string subject, string body, List<string> recipients)
        {
            var mailMessage = new MailMessage();        
            mailMessage.Body = body;
            mailMessage.Subject = subject;
            mailMessage.From = new MailAddress(SenderEmail);

            foreach (var recipient in recipients)
            {
                mailMessage.To.Add(recipient);
            }
 
            return Send(mailMessage, Credentials);
        }

        public string SendHtmlTrackingEmail(List<string> recipients, CorreioApiModel model)
        {
            var projectPath = Directory.GetCurrentDirectory();
            var solutionPath = projectPath.Substring(0, projectPath.Length - 8);
            var templatePath = solutionPath + "Application/Services/EmailProvider/Templates/PackageTracking.cshtml";

            Guid key = Guid.NewGuid();

            string html;

            try
            {
                var template = File.ReadAllText(templatePath);

                html = Engine.Razor.RunCompile(template, key.ToString(), model.GetType(), model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return "Fail";
            }

            AlternateView view = AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);

            LinkedResource facebookIcon = new
                LinkedResource(projectPath + @"\wwwroot\img\emailSac\facebook-icon.png");
            LinkedResource youtubeIcon = new
                LinkedResource(projectPath + @"\wwwroot\img\emailSac\youtube-icon.png");
            LinkedResource instagramIcon = new
                LinkedResource(projectPath + @"\wwwroot\img\emailSac\instagram-icon.png");
            LinkedResource telefoneIcon = new
                LinkedResource(projectPath + @"\wwwroot\img\emailSac\facebook-icon.png");
            LinkedResource logo = new
                LinkedResource(projectPath + @"\wwwroot\img\emailSac\logo-organnact.png");
            LinkedResource logoBranco = new
                LinkedResource(projectPath + @"\wwwroot\img\emailSac\logo-organnact-branco.png");

            facebookIcon.ContentId = "facebookId";
            youtubeIcon.ContentId = "youtubeId";
            instagramIcon.ContentId = "instagramId";
            telefoneIcon.ContentId = "telefoneId";
            logo.ContentId = "logoId";
            logoBranco.ContentId = "logoBrancoId";

            view.LinkedResources.Add(facebookIcon);
            view.LinkedResources.Add(youtubeIcon);
            view.LinkedResources.Add(instagramIcon);
            view.LinkedResources.Add(telefoneIcon);
            view.LinkedResources.Add(logo);
            view.LinkedResources.Add(logoBranco);

            var mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;
            //mailMessage.Body = html;
            mailMessage.Subject = "Movimentação Encomenda";
            mailMessage.From = new MailAddress(SenderEmail);
            mailMessage.AlternateViews.Add(view);

            foreach (var recipient in recipients)
            {
                mailMessage.To.Add(recipient);
            }

            return Send(mailMessage, Credentials);
        }
    }
}