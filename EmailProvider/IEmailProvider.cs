using Application.Models.RastreioSAC;
using System;
using System.Collections.Generic;

namespace Application.Services.EmailService
{
    public interface IEmailProvider
    {
        public string SendMeetingInvite(string subject, DateTime startTime, double duration, List<string> emails, string description);    
        public string SendEmail(string subject, string body, List<string> recipients);
        public string SendHtmlTrackingEmail(List<string> recipients, CorreioApiModel model);
    }
}