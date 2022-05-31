using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YosioluwaOsibemekun.Email
{
    public interface IMailService
    {
        Task<MailResponse> SendMail(MailContent mailContent);
        Task<MailResponse> QuickReply(string name, string tomail, string email, string password);
    }
}
