using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data;
using BankingCore.Users;
using BankingCore.DataAccess;

namespace BankingCoreTests.DataAccessTests
{
    [TestFixture]
    public class DataRepositoryTests
    {

        DataRepository _dataRepo;

        [TestFixtureSetUp]
        public void Setup()
        {
            _dataRepo = DataRepository.GetInstance;
            decimal pointer = 1M;
            foreach (var item in _dataRepo.CustomerList)
            {
                item.DebitBalance(pointer);
                pointer += 5M;
        }

            }
        [Test]
        public void CanGetListOfCustomersTest()
        {
            List<Customer> customerList = _dataRepo.CustomerList;
            Assert.That(customerList, Is.Not.Null);
            Assert.That(customerList.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetCustomerByAccountIdTest()
        {
            int accountNumber = 111;
            Customer customer = _dataRepo.GetCustomerByAccountId(accountNumber);
            Assert.That(customer.LastName, Is.EqualTo("Doe"));
        }

        [Test]
        public void CanGetTopFiveHighestBalanceCustomersTest()
        {
            List<Customer> topFiveList = _dataRepo.GetTopFiveBalanceCustomers();
            Assert.That(topFiveList.Count, Is.EqualTo(5));
            Assert.That(topFiveList[0].Balance, Is.GreaterThan(
                topFiveList[1].Balance));

        }
        [Test]
        public void CanGetBottomFiveBalanceCustomersTest()
        {
            Console.WriteLine(Guid.NewGuid());
            List<Customer> bottomFiveList =
                _dataRepo.GetBottomFiveBalanceCustomers();
            Assert.That(bottomFiveList[4].Balance, Is.LessThan(
                _dataRepo.GetTopFiveBalanceCustomers()[4].Balance));

        } 
    }
}
