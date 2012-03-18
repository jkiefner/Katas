using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BankingCore.Users;
using BankingCore.Accounts;

namespace BankingCoreTests.Users
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void CanInstantiateCustomerTest()
        {
            Customer newCustomer = new Customer();
            newCustomer.FirstName = "John";
            newCustomer.LastName = "Smith";
            int accountID = 12343;
            newCustomer.CurrencyType = CurrencyType.USDollar;
            newCustomer.AccountNumber = accountID;
            Assert.That(newCustomer, Is.Not.Null);
        }
    }
}
