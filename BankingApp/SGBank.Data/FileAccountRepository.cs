using SGBank.Models;
using SGBank.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {

        public Account LoadAccount(string account)
        {
            Account output = null;

            List<Account> accounts = List();

            foreach(Account a in accounts)
            {
                if(a.AccountNumber == account)
                {
                    output = a;
                }
            }
            return output;
        }

        public void SaveAccount(Account account)
        {
            List<Account> accounts = List();

            foreach(Account a in accounts)
            {
                if (a.AccountNumber == account.AccountNumber)
                {
                    a.Name = account.Name;
                    a.Balance = account.Balance;
                    a.Type = account.Type;
                }
            }

            CreateAccountFileRepository(accounts);
        }

        public List<Account> List()
        {
            List<Account> accounts = new List<Account>();

            using (StreamReader sr = new StreamReader(Settings.FilePath))
            {
                sr.ReadLine();
                string line;

                while((line = sr.ReadLine()) != null)
                {
                    Account newAccount = new Account();

                    string[] columns = line.Split(',');

                    newAccount.AccountNumber = columns[0];
                    newAccount.Name = columns[1];
                    newAccount.Balance = decimal.Parse(columns[2]);
                    
                    switch(columns[3])
                    {
                        case "F":
                            newAccount.Type = AccountType.Free;
                            break;
                        case "B":
                            newAccount.Type = AccountType.Basic;
                            break;
                        case "P":
                            newAccount.Type = AccountType.Premium;
                            break;
                        default:
                            throw new Exception($"Error: Invalid AccountType registered to account {newAccount.AccountNumber}");
                    }

                    accounts.Add(newAccount);
                }
                return accounts;
            }
        }

        public string CreateCSVForAccount(Account account)
        {
            string accountType = "INVALID";

            switch(account.Type)
            {
                case AccountType.Free:
                    accountType = "F";
                    break;
                case AccountType.Basic:
                    accountType = "B";
                    break;
                case AccountType.Premium:
                    accountType = "P";
                    break;

            }
            return string.Format("{0},{1},{2},{3}", account.AccountNumber, account.Name, account.Balance, accountType);
        }

        private void CreateAccountFileRepository(List<Account> accounts)
        {
            if (File.Exists(Settings.FilePath))
                File.Delete(Settings.FilePath);

            using (StreamWriter sw = new StreamWriter(Settings.FilePath))
            {
                sw.WriteLine("AccountNumber,Name,Balance,Type");
                foreach(var account in accounts)
                {
                    sw.WriteLine(CreateCSVForAccount(account));
                }
            }
        }

        public void Add(Account account)
        {
            using (StreamWriter sw = new StreamWriter(Settings.FilePath, true))
            {
                string line = CreateCSVForAccount(account);
                sw.WriteLine(line);
            }
        }

        public void Delete(string accountNumber)
        {
            throw new NotImplementedException();
        }
    }
}
