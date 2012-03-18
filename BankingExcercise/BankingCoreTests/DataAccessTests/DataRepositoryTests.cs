using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;

namespace BankingCoreTests.DataAccessTests
{
    [TestFixture]
    public class DataRepositoryTests
    {
        [Test]
        public void CanLoadDataXMLTest()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("Customers");
            ds.Tables["Customers"].Columns.Add("AccountNumber", typeof(int));
            ds.Tables["Customers"].Columns.Add("FirstName", typeof(string));
            ds.Tables["Customers"].Columns.Add("LastName", typeof(string));
            ds.Tables["Customers"].Columns.Add("CurrencyType", typeof(BankingCore.Accounts.CurrencyType));
            
            Assert.That(ds, Is.Not.Null);
        }
        
    }
}
