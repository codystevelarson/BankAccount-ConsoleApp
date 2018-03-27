using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Models.Responses
{
    public class AddAccountResponse : Response
    {
        public Account account { get; set; }
    }
}
