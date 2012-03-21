using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingCore.DataAccess;

namespace Bankingexercise
{
    public class Bankingexercise
    {
        public static DataRepository _dataRepository;
        static void Main(string[] args)
        {
            _dataRepository = DataRepository.GetInstance;
            RunBankingOperation();
        }

        private static void RunBankingOperation()
        {
            HandleStartUpScreen();
            Console.ReadKey();
        }
        private static void HandleStartUpScreen()
        {
            Console.WriteLine("Welcome to Super Bank Terminal\r\n");
            Console.WriteLine("Please choose your customer type:\r\n");
            Console.WriteLine("Bank Manager (A)");
            Console.WriteLine("Customer (B)");

            string keyInput = string.Empty;
            int attemptCounter = 0;

            while (attemptCounter < 3)
            {
                keyInput = Console.ReadKey(true)
               .KeyChar.ToString().ToUpper();
                if (keyInput != "A" && keyInput != "B")
                {
                    Console.WriteLine("Please enter \"A\" or \"B\"");
                    attemptCounter += 1;
                    continue;
                }
                else
                {
                    return;
                }
            }
            switch (keyInput)
            {
                case "A":
                    DisplayManagersMainScreen();
                    break;
                case "B":
                    DisplayCustomerMainScreen();
                    break;
                default:
                    HandleStartUpScreen();
                    break;
            }

        }
        private static void DisplayManagersMainScreen()
        {
            throw new NotImplementedException();
        }
        private static void DisplayCustomerMainScreen()
        {
            throw new NotImplementedException();
        }
    }
}
