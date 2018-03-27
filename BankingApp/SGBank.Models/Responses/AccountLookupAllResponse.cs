using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Models.Responses
{
    public class AccountLookupAllResponse : Response
    {
        public List<Account> Accounts { get; set; }

    }
}
