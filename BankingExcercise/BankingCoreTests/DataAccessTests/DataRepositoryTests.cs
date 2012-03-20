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
            List<Customer> customerList =_dataRepo.CustomerList;
            Assert.That(customerList, Is.Not.Null);
            Assert.That(customerList.Count, Is.GreaterThan(0));
        }

    }
}
