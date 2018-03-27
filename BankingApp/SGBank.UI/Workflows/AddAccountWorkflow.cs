using SGBank.BLL;
using SGBank.Models;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI.Workflows
{
    public class AddAccountWorkflow
    {
        public void Execute()
        {
            AccountManager manager = AccountManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("::::::::::Add Account:::::::::");
            Console.WriteLine(ConsoleIO.DividingBar);
            string accountNumber = ConsoleIO.GetStringInputFromUser("Enter new account number: ");
            string name = ConsoleIO.GetStringInputFromUser("Enter new account holder's name: ");
            string accountType = ConsoleIO.GetStringInputFromUser("Account types: \nF - Free \nB - Basic \nP - Premium \nEnter Account Type: ").ToUpper();

            //PRINT INFO AND ASK IF CORRECT
            Console.WriteLine("NEW ACCOUNT");
            Console.WriteLine(ConsoleIO.DividingBar);
            Console.WriteLine($"Account Number: {accountNumber}");
            Console.WriteLine($"Account Name: {name}");
            Console.WriteLine($"Account Type: {accountType}");
            Console.WriteLine(ConsoleIO.DividingBar);
            ConsoleIO.GetYesNoAnswerFromUser("\nIs this information correct?(Y/N)");


            //TRY TO ADD ACCOUNT
            AddAccountResponse response = manager.AddAccount(accountNumber, name, accountType);

            if(response.Success)
            {
                Console.WriteLine("Account Added!");
                ConsoleIO.DisplayAccountDetails(response.account);
                if (response.account.Type == AccountType.Premium)
                {
                    Console.WriteLine($"{response.account.Name} receives a free Toast-O-Matic Toaster with new Premium account!");
                }
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
