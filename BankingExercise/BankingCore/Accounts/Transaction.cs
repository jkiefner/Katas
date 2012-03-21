using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingCore.Accounts
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public Guid TransactionId { get; set; }
        public int AccountNumber { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal PriorBalance { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; }
    }
}
