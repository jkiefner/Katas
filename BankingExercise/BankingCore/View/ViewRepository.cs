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
            return FormatOutGrid(topFiveList,outString);
        }

        public string GetBottomFiveCustomersByBalanceView()
        {
             List<Customer> bottomFiveList =
                _dataRepo.GetBottomFiveBalanceCustomers();
            StringBuilder outString = new StringBuilder();
            outString.AppendLine("\r\nBottom Five Customers By Balance");
            outString.AppendLine("Account #\tName\t\tBalance");
            return FormatOutGrid(bottomFiveList,outString);
        }

        private string FormatOutGrid(List<Customer> customerList
            ,StringBuilder outputString)
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
    }
}
