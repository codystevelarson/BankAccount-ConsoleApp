using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Models.Interfaces
{
    public interface IAccountRepository
    {
        Account LoadAccount(string accountNumber);
        List<Account> List();
        void SaveAccount(Account account);
        void Add(Account account);
        void Delete(string accountNumber);
    }
}
