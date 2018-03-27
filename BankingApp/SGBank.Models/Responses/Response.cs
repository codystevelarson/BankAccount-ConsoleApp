using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Models.Responses
{
    //Parent class for all responses
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
