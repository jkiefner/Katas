using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingCore.Accounts;

namespace BankingCore.Users
{
    public class Customer
    {

        #region "Properties"

        private CurrencyType _CurrencyType = CurrencyType.USDollar;

        private List<Transaction> _transactionHistory =
            new List<Transaction>();
        public List<Transaction> TransactionHistory
        {
            get
            {
                return _transactionHistory;
            }
            set
            {
                _transactionHistory = value;
            }
        }

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

        private string _LastName;
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

        private int _AccountNumber;
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

        public decimal? _balance { get; private set; }
        public decimal Balance
        {
            get
            {
                CheckForInitialBalance();
                return (decimal)_balance * GetExchangeRateFactor();
            }
        }

        #endregion
              
        public decimal GetExchangeRateFactor()
        {
            switch (_CurrencyType)
            {
                case CurrencyType.USDollar:
                    return 1M;
                    break;
                case CurrencyType.Euro:
                    return 0.5M;
                    break;
                default:
                    return 1M;
                    break;
            }
        }

        private void CalculateInitialBalance()
        {
            _balance = (decimal)TransactionHistory
                .OrderByDescending(x => x.Date)
                .Select(y => y.Balance)
                .FirstOrDefault();
        }

        public bool DebitBalance(decimal amountToDebit)
        {
            CheckForInitialBalance();

            TransactionHistory.Add(new Transaction
            {
                AccountNumber = _AccountNumber,
                Date = DateTime.Now,
                Balance = (decimal)_balance + amountToDebit,
                PriorBalance = (decimal)_balance,
                TransactionAmount = amountToDebit,
                TransactionId = Guid.NewGuid()
            });

            _balance += amountToDebit;
            return true;
        }

        private void CheckForInitialBalance()
        {
            if (_balance == null)
            {
                CalculateInitialBalance();
            }
        }

        public bool CreditBalance(decimal amountToCredit)
        {
            CheckForInitialBalance();

                TransactionHistory.Add(new Transaction
                {
                    AccountNumber = _AccountNumber,
                    Date = DateTime.Now,
                    Balance = (decimal)_balance - amountToCredit,
                    PriorBalance = (decimal)_balance,
                    TransactionAmount = amountToCredit,
                    TransactionId = Guid.NewGuid()
                });
                _balance -= amountToCredit;
                return true;
        }

        public void AddTransaction(Transaction transAction)
        {
            TransactionHistory.Add(transAction);
        }
    }
}
