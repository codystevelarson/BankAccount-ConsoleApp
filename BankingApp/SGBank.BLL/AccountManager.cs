using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL
{
    public class AccountManager 
    {
        private IAccountRepository _accountRepository;

        //whenever an account manager is instantiated, it must take in a IAccountRepository Interface
        public AccountManager(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public AccountLookupAllResponse LookupAllAccounts()
        {
            AccountLookupAllResponse response = new AccountLookupAllResponse();

            response.Accounts = _accountRepository.List();

            if(response.Accounts.Count() == 0)
            {
                response.Success = false;
                response.Message = "No accounts were loaded into the repository";
                return response;
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        //We want to not send back an object but a response to deal with sending this data somewhere else
        public AccountLookupResponse LookupAccount(string accountNumber)
        {
            AccountLookupResponse response = new AccountLookupResponse();
            
            response.Account = _accountRepository.LoadAccount(accountNumber);

            if(response.Account == null)
            {
                response.Success = false;
                response.Message = $"{accountNumber} is not a valid account";
                return response;
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        public AccountDepositResponse Deposit(string accountNumber, decimal amount)
        {
            AccountDepositResponse response = new AccountDepositResponse();

            response.Account = _accountRepository.LoadAccount(accountNumber);

            if (response.Account == null)
            {
                response.Success = false;
                response.Message = $"{accountNumber} is not a valid account";
                return response;
            }
            else
            {
                response.Success = true;
            }

            IDeposit depositRule = DepositRulesFactory.Create(response.Account.Type);
            response = depositRule.Deposit(response.Account, amount);

            if(response.Success)
            {
                _accountRepository.SaveAccount(response.Account);
            }
            return response;
        }

        public AccountWithdrawResponse Withdraw(string accountNumber, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            response.Account = _accountRepository.LoadAccount(accountNumber);

            if(response.Account == null)
            {
                response.Success = false;
                response.Message = $"{accountNumber} is not a valid account.";
                return response;
            }
            else
            {
                response.Success = true;
            }

            IWithdraw withdrawRule = WithdrawRulesFactory.Create(response.Account.Type);

            response = withdrawRule.Withdraw(response.Account, amount);

            if(response.Success)
            {
                _accountRepository.SaveAccount(response.Account);
            }
            return response;
        }

        public AddAccountResponse AddAccount(string accountNumber, string name, string accountType)
        {
            AddAccountResponse response = new AddAccountResponse();

            if(_accountRepository.List().Any(n => n.AccountNumber == accountNumber))
            {
                response.Success = false;
                response.Message = "Error: Account number entered is already an account.\nAccount Numbers must be unique";
                return response;
            }

            response.account = new Account(); 

            switch (accountType)
            {
                case "F":
                case "FREE":
                    response.account.Type = AccountType.Free;
                    break;
                case "B":
                case "BASIC":
                    response.account.Type = AccountType.Basic;
                    break;
                case "P":
                case "PREMIUM":
                    response.account.Type = AccountType.Premium;
                    break;
                default:
                    response.Success = false;
                    response.Message = $"Error: {accountType} is not a valid account type.";
                    return response;
            }

            response.Success = true;
            response.account.AccountNumber = accountNumber;
            response.account.Name = name;

            _accountRepository.Add(response.account);
            _accountRepository.SaveAccount(response.account);

            return response;
        }
    }
}
