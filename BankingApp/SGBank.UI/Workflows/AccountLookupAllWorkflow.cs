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
    public class AccountLookupAllWorkflow
    {
        public void Execute()
        {
            AccountManager manager = AccountManagerFactory.Create();
            AccountLookupAllResponse response = manager.LookupAllAccounts();

            Console.Clear();            

            if (response.Success)
            {
                var accountFilter = response.Accounts.Select(a => a);

                bool validInput = false;
                while(!validInput)
                {
                    Console.WriteLine(":::::::::ALL ACCOUNTS:::::::::");
                    ConsoleIO.HeadingLable("Sort by:");
                    Console.WriteLine("1. Account Number");
                    Console.WriteLine("2. Name");
                    Console.WriteLine("3. Balance: Low  -> High");
                    Console.WriteLine("4. Balance: High -> Low");
                    Console.WriteLine("5. Account Type");
                    Console.WriteLine("6. Overdrafted Accounts");

                    Console.WriteLine("\nM - Main Menu");
                    string userInput = ConsoleIO.GetStringInputFromUser("Enter sorting option: ").ToUpper();

                    Console.Clear();

                    switch (userInput)
                    {
                        case "1":
                        case "ACCOUNT NUMBER":
                        case "AN":
                            accountFilter = response.Accounts.OrderBy(aN => aN.AccountNumber);
                            ConsoleIO.HeadingLable("Accounts by Number:");
                            validInput = true;
                            break;
                        case "2":
                        case "NAME":
                        case "N":
                            accountFilter = response.Accounts.OrderBy(aN => aN.Name);
                            ConsoleIO.HeadingLable("Accounts by Name");
                            validInput = true;
                            break;
                        case "3":
                        case "BALANCE LOW":
                        case "BL":
                            accountFilter = response.Accounts.OrderBy(aN => aN.Balance);
                            ConsoleIO.HeadingLable("Account by Balance: Low -> High");
                            validInput = true;
                            break;
                        case "4":
                        case "BALANCE HIGH":
                        case "BH":
                            accountFilter = response.Accounts.OrderByDescending(aN => aN.Balance);
                            ConsoleIO.HeadingLable("Accounts by Balance: High -> Low");
                            validInput = true;
                            break;
                        case "5":
                        case "ACOUNT TYPE":
                        case "AT":
                            string input = ConsoleIO.GetStringInputFromUser("Account types: \nF - Free \nB - Basic \nP - Premium \nEnter Account Type: ").ToUpper();
                            switch(input)
                            {
                                case "F":
                                case "FREE":
                                    accountFilter = response.Accounts.Where(t => t.Type == AccountType.Free);
                                    ConsoleIO.HeadingLable("Free Accounts");
                                    validInput = true;
                                    break;
                                case "B":
                                case "BASIC":
                                    accountFilter = response.Accounts.Where(t => t.Type == AccountType.Basic);
                                    ConsoleIO.HeadingLable("Basic Accounts");
                                    validInput = true;
                                    break;
                                case "P":
                                case "PREMIUM":
                                    accountFilter = response.Accounts.Where(t => t.Type == AccountType.Premium);
                                    ConsoleIO.HeadingLable("Premium Accounts");
                                    validInput = true;
                                    break;
                                case "A":
                                default:
                                    ConsoleIO.HeadingLable("All Accounts by type:");
                                    accountFilter = response.Accounts.OrderBy(t => t.Type);
                                    break;
                            }                            
                            break;
                        case "6":
                            accountFilter = response.Accounts.Where(a => a.Balance < 0);
                            if(!accountFilter.Any())
                            {
                                ConsoleIO.HeadingLable("All acounts in the black!");
                            }
                            else
                            {
                                ConsoleIO.HeadingLable("Overdrafted Accounts:");
                            }
                            validInput = true;
                            break;
                        case "M":
                            return;
                    }

                }
                
                foreach (var a in accountFilter)
                {
                    ConsoleIO.DisplayAccountDetails(a);
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("\nAn error occured: ");
                Console.WriteLine(response.Message);
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();

        }
    }
}
