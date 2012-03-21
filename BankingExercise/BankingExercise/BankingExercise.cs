using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingCore.DataAccess;
using BankingCore.View;

namespace Bankingexercise
{
    public class Bankingexercise
    {
        private static DataRepository _dataRepository;
        private static ViewRepository _viewRepo;
        static void Main(string[] args)
        {
            _dataRepository = DataRepository.GetInstance;
            _viewRepo = new ViewRepository(_dataRepository);

            RunBankingOperation();
        }

        private static void RunBankingOperation()
        {
            DisplayStartUpScreen();
        }
        private static void DisplayStartUpScreen()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Super Bank Terminal");
            Console.WriteLine("(Please set your terminal's font to Lucida Console for Euro symbol)\r\n");
            Console.WriteLine("Please choose your customer type:\r\n");
            Console.WriteLine("Bank Manager\t(1)");
            Console.WriteLine("Customer\t(2)");
            Console.WriteLine("Exit\t\t(9)");

            bool intValueSelected = false; 
            int attemptCounter = 0;
            int keySelection = 9;

            while (attemptCounter < 3)
            {
                intValueSelected = Int32.TryParse(
                    Console.ReadKey(true).KeyChar.ToString()
                    ,out keySelection);

               if (!intValueSelected)
                {
                    Console.WriteLine("Please enter a numeric value");
                    attemptCounter += 1;
                    continue;
                }
                else
                {
                    break;
                }
            }
            switch (keySelection)
            {
                case 1:
                    DisplayManagersMainScreen();
                    break;
                case 2:
                    DisplayCustomerMainScreen();
                    break;
                case 9:
                    break;
                default:
                    DisplayStartUpScreen();
                    break;
            }

        }
        private static void DisplayManagersMainScreen()
        {
            Console.WriteLine("\r\nPlease Choose from the follwoing options:\r\n");
            Console.WriteLine("List top 5 customers by balance (1)");
            Console.WriteLine("List bottom 5 customers by balance (2)");
            Console.WriteLine("View customer by account number (3)");
            Console.WriteLine("View customer balance by date (4)");
            Console.WriteLine("View all customers (5)");
            Console.WriteLine("Exit to main menu (9)");

            int keyInput;
            Int32.TryParse(Console.ReadKey(true).KeyChar.ToString(),out keyInput);

            if (keyInput != null)
            {
                DisplayManagerSubScreen(keyInput);
            }

        }
        private static void DisplayManagerSubScreen(int keyInput)
        {
            switch (keyInput)
            {
                case 1:
                    DisplayTopFiveByBalanceScreen();
                    break;
                case 2:
                    DisplayBottomFiveByBalanceScreen();
                    break;
                case 3:
                    ViewCustomerByAccountNumberDisplay();
                    break;
                case 4:
                    ViewCustomerByBalanceAndDateDisplay();
                    break;
                case 5:
                    ViewAllCustomersDisplay();
                    break;
                case 9:
                    DisplayStartUpScreen();
                    break;
                default:
                    DisplayManagersMainScreen();
                    break;
            }
        }
        private static void DisplayCustomerMainScreen()
        {
            Console.WriteLine("display customers");
            Console.ReadKey();
        }
        private static void DisplayBottomFiveByBalanceScreen()
        {
            Console.WriteLine(_viewRepo.GetBottomFiveCustomersByBalanceView());
            Console.WriteLine("Enter any key...");
            Console.ReadKey();
            DisplayManagersMainScreen();
        }
        private static void ViewCustomerByAccountNumberDisplay()
        {
            Console.WriteLine("ViewCustomerByAccountNumberDisplay");
            Console.ReadKey();
        }
        private static void ViewCustomerByBalanceAndDateDisplay()
        {
            Console.WriteLine("ViewCustomerByBalanceAndDateDisplay");
            Console.ReadKey();
        }
        private static void ViewAllCustomersDisplay()
        {
            Console.WriteLine("ViewAllCustomersDisplay");
            Console.ReadKey();
        }
        private static void DisplayTopFiveByBalanceScreen()
        {
            Console.WriteLine(_viewRepo.GetTopFiveCustomersByBalanceView());
            Console.WriteLine("Enter any key...");
            Console.ReadKey();
            DisplayManagersMainScreen();
        }
    }
}
