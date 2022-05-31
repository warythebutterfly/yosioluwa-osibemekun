using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YosioluwaOsibemekun.Email
{
    public class MailContent
    {
        public string clientName { get; set; }
        public string clientEmail { get; set; }
        public string clientPassword { get; set; }
        public string email { get; set; }
        public string ccemail { get; set; } = "";
        public string bccemail { get; set; } = "";
        public string subject { get; set; }
        public string message { get; set; }
    }
}
