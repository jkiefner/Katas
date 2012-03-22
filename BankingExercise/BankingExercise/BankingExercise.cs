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
					return;
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
			ViewCustomerByAccountNumberDisplay(MenuType.Customer, isReload);

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
		private static void DisplayCustomerSubScreen(int keyInput)
		{
			switch (keyInput)
			{
				case 1:
					WithdrawCustomerMoneyDisplay();
					break;
				case 2:
					break;
				case 3:
					break;
				case 4:
					break;
				case 9:
					DisplayStartUpScreen();
					break;
				default:
					DisplayStartUpScreen();
					break;
			}
		}

		private static void WithdrawCustomerMoneyDisplay()
		{
			Console.WriteLine("Please enter amount to withdraw:");
			decimal keyInput;
			bool parseSuccessful =
			decimal.TryParse(Console.ReadLine().ToString(), out keyInput);

			while (!parseSuccessful)
			{
				Console.WriteLine("Please enter a numeric value greater than 0 or (x) to exit:");
				if (Console.ReadLine().ToString() == "x")
				{
					DisplayCustomerMainScreen(true);
				}
				else
				{
					decimal.TryParse(Console.ReadLine().ToString(), out keyInput);
					continue;
				}
			}
			if (keyInput == 0M)
			{
				Console.WriteLine("Amount greater than 0 please.");
				WithdrawCustomerMoneyDisplay();
			}
			if (_viewRepo.WithDrawMoneyFromAccountView(_customerAccountNumber, keyInput))
			{
				Console.WriteLine(string.Format("{0} Successfully deposited.", keyInput));
				DisplayCustomerMainScreen(true);
			}
			else
			{
				Console.WriteLine("There was a problem withdrawing from you're account.\r\nPlease check you're balance");
				DisplayCustomerMainScreen(true);
			}
		}

		private static void DisplayBottomFiveByBalanceScreen()
		{
			Console.WriteLine(_viewRepo.GetBottomFiveCustomersByBalanceView());
			Console.WriteLine("Enter any key...");
			Console.ReadKey();
			DisplayManagersMainScreen();
		}

		private static void ViewCustomerByAccountNumberDisplay(MenuType customerType, bool isReload)
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
					if (lineInput.Contains("x"))
					{
						switch (customerType)
						{
							case MenuType.BankManager:
								DisplayManagersMainScreen();
								break;
							case MenuType.Customer:
								_customerAccountNumber = 0;
								DisplayStartUpScreen();
								break;
						}
					}
					continue;
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
				}
				Console.WriteLine(accountLookupResult);
				Console.WriteLine("Please press any key...");
				Console.ReadKey();
				DisplayManagersMainScreen();
			}
			else
			{
				if (accountLookupResult.Contains("not found"))
				{
					Console.Write(accountLookupResult);
					ViewCustomerByAccountNumberDisplay(MenuType.Customer, false);
				}
				else
				{
					_customerAccountNumber = keySelection;
					Console.WriteLine(accountLookupResult);
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
					continue;
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
