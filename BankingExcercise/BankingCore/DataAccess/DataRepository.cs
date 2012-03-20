using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BankingCore.Users;

namespace BankingCore.DataAccess
{
   public class DataRepository
    {
       public DataSet LoadDataSetFromXML()
       {
           DataSet returnData =
               new DataSet();
           returnData.ReadXml("Customers.xml", XmlReadMode.ReadSchema);
           return returnData;
       }

       public List<Customer> GetListOfCustomers()
       {
           DataTable customerTable = LoadDataSetFromXML().Tables["Customers"];
           List<Customer> customerList = new List<Customer>();
           for (int i = 0; i < customerTable.Rows.Count; i++)
           {
               customerList.Add(new Customer(
                   (decimal)customerTable.Rows[i]["Balance"]) { 
               AccountNumber = (int)customerTable.Rows[i]["AccountNumber"],
               FirstName = (string)customerTable.Rows[i]["FirstName"],
               LastName = (string)customerTable.Rows[i]["LastName"],
               CurrencyType = (Accounts.CurrencyType)customerTable.Rows[i]["CurrencyType"]
               });
           }
           return customerList;
       }
    }
}
