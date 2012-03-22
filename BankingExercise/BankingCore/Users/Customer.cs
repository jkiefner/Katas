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
				return (decimal)_balance * ExchangeRate.GetExchangeRateFactor(_CurrencyType);
            }
        }

        #endregion
              
        private void CalculateInitialBalance()
        {
            _balance = (decimal)TransactionHistory
                .OrderByDescending(x => x.Date)
                .Select(y => y.Balance)
                .FirstOrDefault();
        }

        public bool DepositMoney(decimal amountToDeposit)
        {
            CheckForInitialBalance();
            TransactionHistory.Add(new Transaction
            {
                AccountNumber = _AccountNumber,
                Date = DateTime.Now,
                Balance = (decimal)_balance + amountToDeposit,
                PriorBalance = (decimal)_balance,
                TransactionAmount = amountToDeposit,
                TransactionId = Guid.NewGuid()
            });

            _balance += amountToDeposit;
            return true;
        }

        private void CheckForInitialBalance()
        {
            if (_balance == null)
            {
                CalculateInitialBalance();
            }
        }

        public bool WithdrawMoney(decimal amountToWithdraw)
        {
            CheckForInitialBalance();
			if (Balance >= amountToWithdraw)
			{
				TransactionHistory.Add(new Transaction
				{
					AccountNumber = _AccountNumber,
					Date = DateTime.Now,
					Balance = (decimal)_balance - amountToWithdraw,
					PriorBalance = (decimal)_balance,
					TransactionAmount = amountToWithdraw,
					TransactionId = Guid.NewGuid()
				});
				_balance -= amountToWithdraw;
				return true;
			}
			else
				return false;
                
        }

        public void AddTransaction(Transaction transAction)
        {
            TransactionHistory.Add(transAction);
        }

        public void UpdateBalance(decimal newBalance)
        {
            _balance = newBalance;
        }
    }
}
