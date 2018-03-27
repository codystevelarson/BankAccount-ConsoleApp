using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class BasicAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account
        {
            Name = "Basic Account",
            Balance = 100M,
            AccountNumber = "33333",
            Type = AccountType.Basic
        };

        public void Add(Account account)
        {
            throw new NotImplementedException();
        }

        public void Delete(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public Account LoadAccount(string accountNumber)
        {
            if (accountNumber != _account.AccountNumber)
            {
                return null;
            }
            else
            {
                return _account;
            }
        }

        public List<Account> List()
        {
            List<Account> accounts = new List<Account>();
            accounts.Add(_account);
            return accounts;
        }

        public void SaveAccount(Account account)
        {
            _account = account;
        }
    }
}
