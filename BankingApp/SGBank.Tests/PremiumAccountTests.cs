using NUnit.Framework;
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

namespace SGBank.Tests
{
    [TestFixture]
    public class PremiumAccountTests
    {
        [TestCase("66666", "Premium Account", 200000, AccountType.Premium, 2500, true)]
        [TestCase("66666", "Premium Account", 200000, AccountType.Premium, -2500, false)]
        [TestCase("66666", "Premium Account", 200000, AccountType.Free, 2500, false)]
        [TestCase("66666", "Premium Account", 200000, AccountType.Basic, 2500, true)]
        public void PremiumAccountDepositTests(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, bool expectedResult)
        {
            IDeposit deposit = DepositRulesFactory.Create(AccountType.Premium);
            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("66666", "Premium Account", 200000, AccountType.Premium, 2000, 180000, false)]
        [TestCase("66666", "Premium Account", 200000, AccountType.Free, -20000, 180000, false)]
        [TestCase("66666", "Premium Account", 200000, AccountType.Basic, -20000, 180000, false)]
        [TestCase("66666", "Premium Account", 200000, AccountType.Premium, -20000, 180000, true)]
        [TestCase("66666", "Premium Account", 200000, AccountType.Premium, -200501, -511, true)]
        public void PremiumAccountWithdrawTests(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newBalance, bool expectedResult)
        {
            IWithdraw withdraw = WithdrawRulesFactory.Create(AccountType.Premium);
            Account account = new Account();

            account.AccountNumber = accountNumber;
            account.Name = name;
            account.Balance = balance;
            account.Type = accountType;

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }

    }
}
