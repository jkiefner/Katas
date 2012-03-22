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
        [Test]
        public void CanDebitCustomerBalanceTest()
        {
            Customer testCustomer = new Customer();
            decimal currentBalance = testCustomer.Balance;
            Assert.That(currentBalance, Is.EqualTo(0));
            decimal amountToDebit = 5.00M;
            testCustomer.DebitBalance(amountToDebit);
            Assert.That(testCustomer.Balance, Is.EqualTo(amountToDebit));
        }

        [Test]
        public void CanCreditCustomerBalanceTest()
        {
            Customer testCustomer = new Customer();
            testCustomer.DebitBalance(10.00M);
            decimal amountToCredit = 5.00M;
            bool successfullCredit =
            testCustomer.CreditBalance(amountToCredit);
            Assert.That(testCustomer.Balance, Is.EqualTo(5.00M));
        }

        [Test]
        public void CurrencyAdjustmentFactorTest()
        {
            Customer testCustomer = new Customer();
            testCustomer.DebitBalance(10.00M);
            Assert.That(testCustomer.Balance, Is.EqualTo(10M));
            testCustomer.CurrencyType = CurrencyType.Euro;
            Assert.That(testCustomer.Balance, Is.EqualTo(5M).Within(0.02));
            testCustomer.CreditBalance(15M);
            testCustomer.CurrencyType = CurrencyType.USDollar;
            Assert.That(testCustomer.Balance, Is.EqualTo(-5M));
        }

        [Test]
        public void CanGetBalanceForCustomerWithTransactionHistoryTest()
        {
            BankingCore.DataAccess.DataRepository _dRepo =
                BankingCore.DataAccess.DataRepository.GetInstance;
            Customer testCustomer = _dRepo.GetCustomerByAccountId(111);
			testCustomer.CurrencyType = CurrencyType.USDollar;
            Assert.That(testCustomer.Balance, Is.EqualTo(_dRepo.CustomerList[0].Balance));
        }
        [Test]
        public void CanGetBalanceForCustomerWithoutTransactionHistoryTest()
        {
            Customer testCustomer = new Customer();
            decimal balance = testCustomer.Balance;
            Assert.That(balance, Is.EqualTo(0));
        }
    }
}
