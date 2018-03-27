using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class PremiumAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account
        {
            Name = "Premium Account",
            Balance = 200000.00M,
            AccountNumber = "66666",
            Type = AccountType.Premium
        };

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

        public void Add(Account account)
        {
            throw new NotImplementedException();
        }

        public void Delete(string accountNumber)
        {
            throw new NotImplementedException();
        }
    }
}
