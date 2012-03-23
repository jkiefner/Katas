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
        private static int _customerAccountNumber = 0;

        static void Main(string[] args)
        {
            _dataRepository = DataRepository.GetInstance;
            _viewRepo = new ViewRepository(_dataRepository);

            RunBankingOperation();
            _dataRepository.SaveDataToXml();
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
                    , out keySelection);

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
                    DisplayCustomerMainScreen(false);
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
            bool parseSuccessful =
            Int32.TryParse(Console.ReadKey(true).KeyChar.ToString(), out keyInput);

            if (parseSuccessful)
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
                    ViewCustomerByAccountNumberDisplay(MenuType.BankManager, false);
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

        private static void DisplayCustomerMainScreen(bool isReload)
        {

            if (ViewCustomerByAccountNumberDisplay(MenuType.Customer, isReload))
            {
                Console.WriteLine("\r\nPlease Choose from the follwoing options:\r\n");
                Console.WriteLine("Withdraw Money from your account\t(1)");
                Console.WriteLine("Deposit Money into your account\t\t(2)");
                Console.WriteLine("Transfer Money to another account\t(3)");
                Console.WriteLine("Change your currency type\t\t(4)");
                Console.WriteLine("Exit to main menu\t\t\t(9)");

                int keyInput;
                bool parseSuccessful =
                Int32.TryParse(Console.ReadKey(true).KeyChar.ToString(), out keyInput);

                if (parseSuccessful)
                {
                    DisplayCustomerSubScreen(keyInput);
                }
            }
            else
            {
                DisplayStartUpScreen();
            }
        }

        private static void DisplayCustomerSubScreen(int keyInput)
        {
            switch (keyInput)
            {
                case 1:
                    WithdrawOrDepositCustomerFunds(true);
                    break;
                case 2:
                    WithdrawOrDepositCustomerFunds(false);
                    break;
                case 3:
                    TransferCustomerFunds();
                    break;
                case 4:
                    ChangeCustomerCurrencyType();
                    break;
                case 9:
                    DisplayStartUpScreen();
                    break;
                default:
                    break;
            }
        }

        private static void TransferCustomerFunds()
        {
            Console.WriteLine(string.Format("Please enter account number to transfer to."));
            int accountNumber;
            bool parseSuccessful = Int32.TryParse(Console.ReadLine().ToString(), out accountNumber);
            bool escapeWasPressed = false;
            while (!parseSuccessful)
            {
                Console.WriteLine("Please enter a numeric value greater than 0 or (x) to exit:");
                string input = Console.ReadLine();
                if (input.ToUpper().Equals("X"))
                {
                    escapeWasPressed = true;
                    parseSuccessful = true;
                }
                else
                {
                    parseSuccessful = Int32.TryParse(input, out accountNumber);
                }
            }
            if (escapeWasPressed)
            {
                DisplayCustomerMainScreen(true);
            }
            else
            {
                if (!_viewRepo.CheckForGoodAccount(accountNumber))
                {
                    Console.WriteLine(string.Format("We're sorry, but {0} is not an active account number.",
                        accountNumber));
                    DisplayCustomerMainScreen(true);
                }
                else
                {
                    Console.WriteLine(string.Format("Please enter amount to transfer."));
                    decimal amountToTransfer;
                    parseSuccessful = decimal.TryParse(Console.ReadLine(), out amountToTransfer);

                    while (!parseSuccessful)
                    {
                        Console.WriteLine("Please enter a numeric value greater than 0 or (x) to exit:");
                        string inputString = Console.ReadLine();
                        if (inputString.ToUpper().Equals("X"))
                        {
                            escapeWasPressed = true;
                            parseSuccessful = true;
                        }
                        else
                        {
                            parseSuccessful = decimal.TryParse(inputString, out amountToTransfer);
                        }
                    }
                    if (escapeWasPressed)
                    {
                        DisplayCustomerMainScreen(true);
                    }
                    else
                    {
                        Console.WriteLine(string.Format("\r\nTransferring {0:C} to account number {1}"
                        , amountToTransfer, accountNumber));
                        Console.WriteLine("Please enter (Y) to continue or (X) to cancel transfer of funds");
                        string inputValue = Console.ReadKey().KeyChar.ToString().ToUpper();
                        bool escapePressed = false;
                        while (!inputValue.Equals("Y"))
                        {
                            if (inputValue.Equals("X"))
                            {
                                escapePressed = true;
                                inputValue = "Y";
                            }
                            else
                            {
                                Console.WriteLine("\r\nPlease enter an \"X\" or \"Y\"");
                                inputValue = Console.ReadKey().KeyChar.ToString().ToUpper();
                            }
                        }
                        if (escapePressed)
                        {
                            DisplayCustomerMainScreen(true);
                        }
                        else
                        {
                            if (inputValue.Equals("Y"))
                            {
                                if (_viewRepo.TransferCustomerFunds(_customerAccountNumber
                                    , accountNumber, amountToTransfer))
                                {
                                    Console.WriteLine(string.Format(
                                        "\r\n{0} dollars was transferred to account {1}", amountToTransfer
                                        , accountNumber));
                                }
                                else
                                {
                                    Console.WriteLine("\r\nThere was a problem transferring the funds. Please check your balance.");
                                }
                                DisplayCustomerMainScreen(true);
                            }
                        }
                    }
                }
            }
        }

        private static void WithdrawOrDepositCustomerFunds(bool withDrawingFunds)
        {
            string actionString = "deposit";
            if (withDrawingFunds)
            {
                actionString = "withdraw";
            }
            Console.WriteLine(string.Format("Please enter amount to {0}:", actionString));
            decimal keyInput;
            bool parseSuccessful =
            decimal.TryParse(Console.ReadLine().ToString(), out keyInput);
            bool escapeWasPressed = false;
            while (!parseSuccessful)
            {
                Console.WriteLine("Please enter a numeric value greater than 0 or (x) to exit:");
                string input = Console.ReadLine();
                if (input.ToUpper().Equals("X"))
                {
                    escapeWasPressed = true;
                    break;
                }
                else
                {
                    parseSuccessful = decimal.TryParse(input, out keyInput);
                }
            }
            if (escapeWasPressed)
            {
                DisplayCustomerMainScreen(true);
            }
            else
            {
                if (keyInput == 0M)
                {
                    Console.WriteLine("Amount greater than 0 please.");
                    WithdrawOrDepositCustomerFunds(withDrawingFunds);
                }
                if (withDrawingFunds)
                {
                    if (_viewRepo.WithDrawMoneyFromAccountView(_customerAccountNumber, keyInput))
                    {
                        Console.WriteLine(string.Format("{0:C} Successfully withdrawn.", keyInput));
                        DisplayCustomerMainScreen(true);
                    }
                    else
                    {
                        Console.WriteLine("There was a problem withdrawing from your account.\r\nPlease check you're balance");
                        DisplayCustomerMainScreen(true);
                    }
                }
                else
                {
                    if (_viewRepo.DepostMoneyIntoAccountView(_customerAccountNumber, keyInput))
                    {
                        Console.WriteLine(string.Format("{0:C} Successfully deposited.", keyInput));
                        DisplayCustomerMainScreen(true);
                    }
                    else
                    {
                        Console.WriteLine("There was a problem depositing into your account.\r\nPlease check you're balance");
                        DisplayCustomerMainScreen(true);
                    }
                }
            }
        }

        private static void DisplayBottomFiveByBalanceScreen()
        {
            Console.WriteLine(_viewRepo.GetBottomFiveCustomersByBalanceView());
            Console.WriteLine("Enter any key...");
            Console.ReadKey();
            DisplayManagersMainScreen();
        }

        private static bool ViewCustomerByAccountNumberDisplay(MenuType customerType, bool isReload)
        {
            bool intValueSelected = false;
            int keySelection = 0;
            if (!isReload)
            {

                Console.WriteLine("Please Enter Customer Account Number.");
                string lineInput = Console.ReadLine();
                intValueSelected = Int32.TryParse(lineInput, out keySelection);

                while (!intValueSelected)
                {
                    Console.WriteLine("Please Enter Customer Account Number or (x) to exit.");
                    lineInput = Console.ReadLine();
                    intValueSelected = Int32.TryParse(lineInput, out keySelection);
                    if (lineInput.ToUpper().Contains("X"))
                    {
                        switch (customerType)
                        {
                            case MenuType.BankManager:
                                DisplayManagersMainScreen();
                                return false;
                            case MenuType.Customer:
                                _customerAccountNumber = 0;
                                return false;
                        }
                    }
                }
            }
            else
            {
                keySelection = _customerAccountNumber;
            }

            string accountLookupResult =
            _viewRepo.GetCustomerViewByAccountNumberView(keySelection);

            if (customerType == MenuType.BankManager)
            {
                if (accountLookupResult.Contains("not found"))
                {
                    Console.Write(accountLookupResult);
                    ViewCustomerByAccountNumberDisplay(MenuType.BankManager, false);
                    return false;
                }
                else
                {
                    Console.WriteLine(accountLookupResult);
                    Console.WriteLine("Please press any key...");
                    Console.ReadKey();
                    DisplayManagersMainScreen();
                    return false;
                }
            }
            else
            {
                if (accountLookupResult.Contains("not found"))
                {
                    Console.Write(accountLookupResult);
                    return false;
                }
                else
                {
                    _customerAccountNumber = keySelection;
                    Console.WriteLine(accountLookupResult);
                    return true;
                }

            }
        }

        private static void ViewCustomerByBalanceAndDateDisplay()
        {
            Console.WriteLine("Please Enter Customer Account Number:");
            bool intValueSelected = false;
            int keySelection = 9;

            intValueSelected = Int32.TryParse(
                Console.ReadLine()
                , out keySelection);

            while (!intValueSelected)
            {
                Console.WriteLine("Please enter a numeric value");
                intValueSelected = Int32.TryParse(
               Console.ReadLine()
               , out keySelection);
                continue;
            }
            if (keySelection == 9)
            {
                DisplayManagersMainScreen();
            }
            else
            {
                Console.WriteLine("Please enter a date in this format mm-dd-yyyy ");
                bool successfullDateInput = false;
                DateTime dateOfInterest;
                successfullDateInput = DateTime.TryParse(
                    Console.ReadLine(), out dateOfInterest);
                while (!successfullDateInput)
                {
                    Console.WriteLine("Please enter a date in this format mm-dd-yyyy ");
                    successfullDateInput = DateTime.TryParse(
                    Console.ReadLine(), out dateOfInterest);
                }
                string accountLookupResult = _viewRepo
                    .GetCustomerViewByAccountAndDate(keySelection, dateOfInterest);
                if (accountLookupResult.Contains("not found"))
                {
                    Console.Write(accountLookupResult);
                    Console.WriteLine("Please enter (9) to exit or");
                    ViewCustomerByBalanceAndDateDisplay();
                }
                else
                {
                    Console.WriteLine(accountLookupResult);
                    Console.WriteLine("Please press any key...");
                    Console.ReadKey();
                    DisplayManagersMainScreen();
                }
            }
        }

        private static void ChangeCustomerCurrencyType()
        {
            Console.WriteLine("Select your currency type from below\r\n");
            Console.WriteLine(_viewRepo.ListCurrencyTypes() + "\r\n");
            bool intValueSelected = false;
            int keySelection;
            intValueSelected = Int32.TryParse(
                Console.ReadKey().KeyChar.ToString()
                , out keySelection);

            while (!intValueSelected)
            {
                Console.WriteLine("Please enter a numeric value listed above");
                intValueSelected = Int32.TryParse(
               Console.ReadKey().KeyChar.ToString()
               , out keySelection);
            }
            if (_viewRepo.ChangeCustomerCurrencyType(_customerAccountNumber
                , keySelection))
            {
                Console.WriteLine("\r\nCurrency Updated.\r\n");
                DisplayCustomerMainScreen(true);
            }
            else
            {
                Console.WriteLine("\r\nThere was an error updating the Currency Type\r\n");
                DisplayCustomerMainScreen(true);
            }

        }

        private static void ViewAllCustomersDisplay()
        {
            Console.WriteLine(_viewRepo.GetAllCustomersView());
            Console.WriteLine("Please press any key...");
            Console.ReadKey();
            DisplayManagersMainScreen();
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
