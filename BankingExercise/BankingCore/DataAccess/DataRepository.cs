using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BankingCore.Users;
using System.Xml.Linq;
using BankingCore.Accounts;

namespace BankingCore.DataAccess
{
	public sealed class DataRepository
	{
		private List<Customer> _customerList;
		public List<Customer> CustomerList
		{
			get
			{
				return _customerList;
			}
			set
			{
				_customerList = value;
			}
		}

		public static DataRepository GetInstance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DataRepository();
				}
				return _instance;
			}
		}

		private static DataRepository _instance;

		private DataRepository()
		{
			_customerList = LoadDataSetFromXML();
		}

		private List<Customer> LoadDataSetFromXML()
		{
			List<Customer> cList = new List<Customer>();
			XDocument xmlData = XDocument.Load("Customers.xml");
			cList = (from items in xmlData.Descendants("Customer")
					 select new Customer()
					 {
						 FirstName = items.Element("FirstName").Value.ToString(),
						 LastName = items.Element("LastName").Value.ToString(),
						 AccountNumber = (int)items.Element("AccountNumber"),
						 CurrencyType = (Accounts.CurrencyType)((int)items.Element("CurrencyType"))
					 }).ToList();

			foreach (var customer in cList)
			{
				customer.TransactionHistory = (from trans in xmlData.Descendants("Transaction")
											   where (int)trans.Element("AccountNumber") == (int)customer.AccountNumber
											   select new Transaction()
											   {
												   TransactionId = (Guid)trans.Element("TransactionGuid"),
												   AccountNumber = (int)trans.Element("AccountNumber"),
												   TransactionAmount = (decimal)trans.Element("Amount"),
												   PriorBalance = (decimal)trans.Element("PriorBalance"),
												   Balance = (decimal)trans.Element("Balance"),
												   Description = trans.Element("Description").Value.ToString()
											   }).ToList();
			}

			return cList;
		}

		public Customer GetCustomerByAccountId(int accountNumber)
		{
			return _customerList
				.Where(x => x.AccountNumber == accountNumber).FirstOrDefault();

		}
		public List<Customer> GetTopFiveBalanceCustomers()
		{
			return _customerList.OrderByDescending(x => x.Balance).Take(5).ToList();
		}

		public List<Customer> GetBottomFiveBalanceCustomers()
		{
			return _customerList.OrderBy(x => x.Balance).Take(5).ToList();
		}

		public Customer GetCustomerByAccountAndDate(int accountNumber
			, DateTime dateOfInterest)
		{
			Customer customerOfInterest = _customerList.Where(
				x => x.AccountNumber == accountNumber).FirstOrDefault<Customer>();
			if (customerOfInterest != null)
			{
				decimal bal = (decimal)((customerOfInterest.TransactionHistory).OrderByDescending(
							   x => x.Date)).Where(y => y.Date <= dateOfInterest)
							   .Select(i => i.Balance).FirstOrDefault();

				customerOfInterest.UpdateBalance(bal);
			}

			return customerOfInterest;
		}

		public void SaveDataToXml()
		{
			System.Reflection.Assembly ass = System.Reflection.Assembly.GetAssembly(typeof(DataRepository));

			XDocument xmlDoc = new XDocument();
			XElement xml = new XElement("Customers",
				from c in _customerList
				orderby c.AccountNumber
				select new XElement("Customer",
					new XElement("AccountNumber", c.AccountNumber),
					new XElement("FirstName", c.FirstName),
					new XElement("LastName", c.LastName),
					new XElement("CurrencyType", (int)c.CurrencyType)));

			List<Transaction> tList = (_customerList).SelectMany(x => x.TransactionHistory).ToList();

			XElement xml2 = new XElement("Transactions",
				from c in tList
				orderby c.AccountNumber
				select new XElement("Transaction",
					new XElement("Date", c.Date.ToShortDateString()),
					new XElement("TransactionGuid", c.TransactionId),
					new XElement("AccountNumber", c.AccountNumber),
					new XElement("Amount", c.TransactionAmount),
					new XElement("PriorBalance", c.PriorBalance),
					new XElement("Balance", c.Balance),
					new XElement("Description", c.Description)));

			XElement xmlAggregate = new XElement("NewDataSet", xml, xml2);

			xmlAggregate.Save("Customers.xml");
		}
	}
}
