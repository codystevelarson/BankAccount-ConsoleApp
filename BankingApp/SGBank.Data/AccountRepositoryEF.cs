using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class AccountRepositoryEF : IAccountRepository
    {

        public void Add(Account account)
        {
            using (var context = new AccountDBEntities())
            {
                context.Accounts.Add(account);
                context.SaveChanges();
            }
        }

        public void Delete(string accountNumber)
        {
            using (var context = new AccountDBEntities())
            {
                context.Accounts.Remove(LoadAccount(accountNumber));
            }
        }

        public List<Account> List()
        {
            List<Account> accounts = new List<Account>();

            using (var context = new AccountDBEntities())
            {
                foreach(var account in context.Accounts)
                {
                    accounts.Add(account);
                }
            }
            return accounts;
        }

        public Account LoadAccount(string accountNumber)
        {
            using (var context = new AccountDBEntities())
            {
                return context.Accounts.SingleOrDefault(a => a.AccountNumber == accountNumber);
            }
        }

        public void SaveAccount(Account account)
        {
            AccountDBEntities entities = new AccountDBEntities();
            entities.Accounts.Attach(account);
            entities.Entry(account).State = EntityState.Modified;
            entities.SaveChanges();  
        }
    }
}
