using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingCore.DataAccess;
using BankingCore.Users;
using System.Globalization;

namespace BankingCore.View
{
    public class ViewRepository
    {
        private DataRepository _dataRepo;
        public ViewRepository(DataRepository dataRepo)
        {
            _dataRepo = dataRepo;
        }

        public string GetTopFiveCustomersByBalanceView()
        {
            List<Customer> topFiveList =
                _dataRepo.GetTopFiveBalanceCustomers();
            StringBuilder outString = new StringBuilder();
            outString.AppendLine("\r\nTop Five Customers By Balance");
            outString.AppendLine("Account #\tName\t\tBalance");
            return FormatSingleAccountGrid(topFiveList, outString);
        }

        public string GetBottomFiveCustomersByBalanceView()
        {
            List<Customer> bottomFiveList =
               _dataRepo.GetBottomFiveBalanceCustomers();
            StringBuilder outString = new StringBuilder();
            outString.AppendLine("\r\nBottom Five Customers By Balance");
            outString.AppendLine("Account #\tName\t\tBalance");
            return FormatSingleAccountGrid(bottomFiveList, outString);
        }

        public string GetAllCustomersView()
        {
            StringBuilder outString = new StringBuilder();
            outString.AppendLine("\r\nAll Customers ");
            outString.AppendLine("Account #\tName\t\tBalance");
            return FormatSingleAccountGrid(_dataRepo.CustomerList, outString);
        }

        public string GetCustomerViewByAccountAndDate(int accountNumber
            , DateTime dateOfInterest)
        {
            Customer customerOfInterest = _dataRepo.GetCustomerByAccountAndDate(
                accountNumber, dateOfInterest);
            StringBuilder outString = new StringBuilder();
            outString.AppendLine(string.Format("\r\nAccount Balance for {0}"
                , dateOfInterest.ToShortDateString()));
            if (customerOfInterest != null)
            {
                FormatSingleCustomerView(customerOfInterest, outString);
            }
            else
            {
                outString.AppendLine(string.Format("Account Number:{0} not found..."
                    , accountNumber));
            }
            return outString.ToString();
        }


        private StringBuilder FormatSingleCustomerView(Customer customer, StringBuilder sBuilder)
        {
            sBuilder.AppendLine("Account #\tName\t\tBalance");
            string formattedBalance = string.Empty;

            if (customer.CurrencyType == Accounts.CurrencyType.Euro)
            {
                formattedBalance = customer.Balance.ToString("c", new CultureInfo("en-IE", false));
            }
            else
                formattedBalance = customer.Balance.ToString("c");
            sBuilder.AppendLine(string.Format
                ("{0}\t\t{1}, {2}\t{3}"
                , customer.AccountNumber.ToString()
                , customer.LastName
                , customer.FirstName
                , formattedBalance));

            return sBuilder;
        }
        public string GetCustomerViewByAccountNumberView(int accountNumber)
        {
            Customer customerByAccount =
               _dataRepo.GetCustomerByAccountId(accountNumber);
            StringBuilder outString = new StringBuilder();
            outString.AppendLine("\r\nCustomer Account Information");
            if (customerByAccount != null)
            {
                FormatSingleCustomerView(customerByAccount, outString);
            }
            else
            {
                outString.AppendLine(string.Format("Account Number:{0} not found..."
                    , accountNumber));
            }
            return outString.ToString();
        }

		public bool DepostMoneyIntoAccountView(int accountNumber,decimal amountToDeposit)
		{
			Customer thisCustomer = _dataRepo.GetCustomerByAccountId(accountNumber);
			return thisCustomer.DepositMoney(amountToDeposit);
		}

		public bool WithDrawMoneyFromAccountView(int accountNumber,decimal amountToWithdraw)
		{
			Customer thisCustomer = _dataRepo.GetCustomerByAccountId(accountNumber);
			return thisCustomer.WithdrawMoney(amountToWithdraw);
		}

        private string FormatSingleAccountGrid(List<Customer> customerList
            , StringBuilder outputString)
        {
            StringBuilder outString = new StringBuilder();

            foreach (var item in customerList)
            {
                string formattedBalance = string.Empty;

                if (item.CurrencyType == Accounts.CurrencyType.Euro)
                {
                    formattedBalance = item.Balance.ToString("c", new CultureInfo("en-IE", false));
                }
                else
                    formattedBalance = item.Balance.ToString("c");
                outString.AppendLine(string.Format
                    ("{0}\t\t{1}, {2}\t{3}"
                    , item.AccountNumber.ToString()
                    , item.LastName
                    , item.FirstName
                    , formattedBalance));
            }
            return outString.ToString();
        }

		public bool CheckForGoodAccount(int accountNumber)
		{
			Customer customerToLookup = _dataRepo.GetCustomerByAccountId(accountNumber);
			if (customerToLookup != null)
			{
				return true;
			}
			else
				return false;
		}
    }
}
