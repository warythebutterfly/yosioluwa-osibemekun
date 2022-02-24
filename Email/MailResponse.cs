using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YosioluwaOsibemekun.Email
{
    public class MailResponse
    {
        public string responseHeader { get; set; }
        public string responseText { get; set; }
        public string success { get; set; }
        public int status { get; set; }
    }
}
