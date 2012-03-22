using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingCore.Users;

namespace BankingCore.Accounts
{
    public class AccountRepository
    {
        public static bool TransferFunds(Customer originatingCustomer,
            Customer destinationCustomer,
            decimal amountOfTransfer)
        {
            if (originatingCustomer.WithdrawMoney(amountOfTransfer))
            {
                if (destinationCustomer
                    .DepositMoney(amountOfTransfer))
                {
                    return true;
                }
                else
                {
                    originatingCustomer.DepositMoney(amountOfTransfer);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }

}
