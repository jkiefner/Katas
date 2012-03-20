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
            _dataRepo = new DataRepository();
        }

        [Test]
        public void CanLoadDataXMLTest()
        {
            DataSet ds = _dataRepo.LoadDataSetFromXML();
            Assert.That(ds.Tables["Customers"].Rows.Count, Is.GreaterThan(0));
            Assert.That(ds.Tables["Customers"].Rows[0]["AccountNumber"], Is.EqualTo(111));
        }

        [Test]
        public void CanGetListOfCustomersTest()
        {
            List<Customer> customerList = _dataRepo.GetListOfCustomers();
            Assert.That(customerList, Is.Not.Null);
            Assert.That(customerList.Count, Is.GreaterThan(0));
        }

    }
}
