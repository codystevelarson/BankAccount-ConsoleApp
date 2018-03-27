﻿using SGBank.BLL;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI.Workflows
{
    public class WithdrawWorkflow
    {
        public void Execute()
        {
            Console.Clear();
            AccountManager accountManager = AccountManagerFactory.Create();

            string accountNumber = ConsoleIO.GetStringInputFromUser("Enter an account number: ");           
            decimal amount = ConsoleIO.GetDecimalFromUser("Enter a withdraw amount: ");

            AccountWithdrawResponse response = accountManager.Withdraw(accountNumber, amount);

            if(response.Success)
            {
                Console.WriteLine("Withdraw Completed!");
                Console.WriteLine($"Account Number: {response.Account.AccountNumber}");
                Console.WriteLine($"Old Balance: {response.OldBalance:c}");
                Console.WriteLine($"Amount Withdrawn: {response.Amount:c}");
                Console.WriteLine($"New Balance: {response.Account.Balance:c}");
            }
            else
            {
                Console.WriteLine("An error occured: ");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

        }
    }
}
