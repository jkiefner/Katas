using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingCore.Users;
using NUnit.Framework;
using BankingCore.Accounts;

namespace BankingCoreTests.Accounts
{
    [TestFixture]
    public class AccountRepositoryTests
    {
        private AccountRepository _repo;

        [TestFixtureSetUp]
        public void Setup()
        {
            _repo = new AccountRepository();
        }
        [Test]
        public void CanTransferMoneyFromOneAccountToAnotherTest()
        {
            Customer originatingCustomer = new Customer
            {
                AccountNumber = 111,
                FirstName = "John",
                LastName = "Doe"
            };
            Customer destinationCustomer = new Customer
            {
                AccountNumber = 222,
                FirstName = "Moe",
                LastName = "Monee"
            };

            originatingCustomer.DebitBalance(100.00M);
            Assert.That(originatingCustomer.Balance, Is.EqualTo(100.00M));
            decimal amountOfTransfer = 10.00M;
            bool transferSuccsessfull =
                _repo.TransferFunds(originatingCustomer
                , destinationCustomer
                , amountOfTransfer);
            Assert.That(transferSuccsessfull, Is.True);
            Assert.That(destinationCustomer.Balance, Is.EqualTo(10.00));
            Assert.That(originatingCustomer.Balance, Is.EqualTo(90.00));
        }
    }
}
