using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YosioluwaOsibemekun.Email
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _config;
        readonly ILogger<MailService> _logger;


        public MailService(ILogger<MailService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
            _url = _config.GetValue<string>("Url");
            _myEmail = _config.GetValue<string>("MyEmail");
            _myEmailPassword = _config.GetValue<string>("MyEmailPassword");

        }

        private string _url;
        private string _myEmail;
        private string _myEmailPassword;

        public async Task<MailResponse> SendMail(MailContent mailContent)
        {
            try
            {
                string name = mailContent.clientName;
                string email = mailContent.email;
                mailContent.message = mailContent.message + " <br/> - This message was sent by " + mailContent.email + "(" + mailContent.clientName + ")";
                mailContent.clientName = "Temitoyosi's contact form";
                mailContent.clientEmail = _myEmail;
                mailContent.email = mailContent.clientEmail;
                mailContent.clientPassword = _myEmailPassword;

                var payload = StringUtility.SerializeData(mailContent);

                var restResponse = await StringUtility.MakeApiRequest(Method.POST, _url, payload);

                var dataContent = restResponse.Content;

                var responseBody = JsonConvert.DeserializeObject<MailResponse>(dataContent);

                if (responseBody.code == "200")
                {
                    await QuickReply(name, email, mailContent.clientEmail, mailContent.clientPassword);

                    return responseBody;
                }
                else throw new ArgumentException(responseBody.message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        public async Task<MailResponse> QuickReply(string name, string tomail, string myEmail, string myPassword)
        {
            try
            {
                MailContent mailContent = new MailContent();
                //mailContent.from = _myEmail;
                mailContent.clientName = "Temitoyosi's Contact Form";
                mailContent.clientEmail = myEmail;
                mailContent.clientPassword = myPassword;
                mailContent.email = tomail;
                mailContent.subject = "Acknowledgement";
                mailContent.message = "Hi " + name + "," +
                    "<br/> Thanks for contacting me :). I would get back to you shortly. <br/> <br/> Regards, <br/> Temitoyosi Osibemekun.";


                var payload = StringUtility.SerializeData(mailContent);

                var restResponse = await StringUtility.MakeApiRequest(Method.POST, _url, payload);

                var dataContent = restResponse.Content;

                var responseBody = JsonConvert.DeserializeObject<MailResponse>(dataContent);

                return responseBody;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

      
    }
}
