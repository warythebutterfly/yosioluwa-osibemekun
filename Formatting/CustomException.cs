using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YosioluwaOsibemekun.Formatting
{
    public class CustomException : ApplicationException
    {
        public CustomException(string message) : base(message)
        {
        }
    }
}
