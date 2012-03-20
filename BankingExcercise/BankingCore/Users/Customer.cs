using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingCore.Accounts;

namespace BankingCore.Users
{
    public class Customer
    {

        public Customer()
            : this(0M)
        {}

        public Customer(decimal initialBalance)
        {
            _balance = initialBalance;
        }

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

        private decimal CalculateExchangeRate()
        {
            switch (_CurrencyType)
            {
                case CurrencyType.USDollar:
                    return 1M;
                    break;
                case CurrencyType.Euro:
                    return 1.5M;
                    break;
                default:
                    return 1M;
                    break;
            }
        }

        private decimal _balance = 0.00M;
        public decimal Balance
        {
            get
            {
                return _balance * CalculateExchangeRate();
            }
        }

        public bool DebitBalance(decimal amountToDebit)
        {
            _balance += amountToDebit;
            return true;
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
