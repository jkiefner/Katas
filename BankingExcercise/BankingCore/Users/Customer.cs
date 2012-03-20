using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingCore.Accounts;

namespace BankingCore.Users
{
    public class Customer
    {

        private CurrencyType _CurrencyType = CurrencyType.USDollar;
        private int _AccountNumber;
        private string _LastName;
        private string _FirstName;
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
            }
        }

        public int AccountNumber
        {
            get
            {
                return _AccountNumber;
            }
            set
            {
                _AccountNumber = value;
            }
        }

        public CurrencyType CurrencyType
        {
            get
            {
                return _CurrencyType;
            }
            set
            {
                _CurrencyType = value;
            }
        }

        private decimal _balance = 0.00M;
        public decimal Balance
        {
            get
            {
                return _balance;
            }            
        }

        public void DebitBalance(decimal amountToDebit)
        {
            _balance += amountToDebit;
        }

        public bool CreditBalance(decimal amountToCredit)
        {
            if (_balance >= amountToCredit)
            {
                _balance -= amountToCredit;
                return true;
            }
            else
                return false;
        }
    }
}
