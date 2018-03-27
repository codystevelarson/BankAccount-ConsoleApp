using SGBank.UI.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI
{
    public static class Menu
    {
        public static void Start()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("SG Bank Application");
                Console.WriteLine(ConsoleIO.DividingBar);
                Console.WriteLine("1. Lookup an Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. List all Accounts");
                Console.WriteLine("5. Add new Account");

                Console.WriteLine("\nQ - To Quit");
                Console.Write("\nEnter Selection: ");

                string userInput = Console.ReadLine().ToUpper();

                switch (userInput)
                {
                    case "1":
                        AccountLookupWorkflow lookupFlow = new AccountLookupWorkflow();
                        lookupFlow.Execute();
                        break;
                    case "2":
                        DepositWorkflow depositFlow = new DepositWorkflow();
                        depositFlow.Execute();
                        break;
                    case "3":
                        WithdrawWorkflow withdrawFlow = new WithdrawWorkflow();
                        withdrawFlow.Execute();
                        break;
                    case "4":
                        AccountLookupAllWorkflow lookupAllFlow = new AccountLookupAllWorkflow();
                        lookupAllFlow.Execute();
                        break;
                    case "5":
                        AddAccountWorkflow addFlow = new AddAccountWorkflow();
                        addFlow.Execute();
                        break;
                    case "Q":
                        return;
                }
            }
        }
    }
}
