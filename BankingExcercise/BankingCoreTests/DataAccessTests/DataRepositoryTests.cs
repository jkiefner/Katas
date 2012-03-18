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
            ds.ReadXml("Customers.xml", XmlReadMode.ReadSchema);
            Assert.That(ds.Tables["Customers"].Rows.Count, Is.GreaterThan(0));
            Assert.That(ds.Tables["Customers"].Rows[0]["AccountNumber"], Is.EqualTo(111));
            Assert.That(ds, Is.Not.Null);
        }
    }
}
