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
        private string _myOtherEmail;
        private string _myEmailPassword;

        public async Task<MailResponse> SendMail(string name, string tomail, string subject, string message)
        {
            try
            {

                MailContent mailContent = new MailContent();
                //mailContent.clientName = name;
                mailContent.email = _myEmail;
                mailContent.subject = subject;
                mailContent.message = message + " <br/> - This message was sent by " + tomail + "(" + name + ")";


                var payload = StringUtility.SerializeData(mailContent);

                var restResponse = await StringUtility.MakeApiRequest(Method.POST, _url, payload);

                var dataContent = restResponse.Content;

                var responseBody = JsonConvert.DeserializeObject<MailResponse>(dataContent);

                if (responseBody.status == 200) await QuickReply(name, tomail);

                return responseBody;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        public async Task<MailResponse> QuickReply(string name, string tomail)
        {
            try
            {

                MailContent mailContent = new MailContent();
                //mailContent.from = _myEmail;
                mailContent.name = "Temitoyosi's Contact Form";
                mailContent.email = tomail;
                mailContent.subject = "Acknowledgement";
                mailContent.message = "Hi " + name + "," +
                    "<br/> Thanks for contacting me :). I would get back to you shortly. <br/> <br/> Regards, <br/> Temitoyosi Osibemekun."; ;


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
