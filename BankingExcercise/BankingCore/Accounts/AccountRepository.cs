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
            if (originatingCustomer.CreditBalance(amountOfTransfer))
            {
                if (destinationCustomer
                    .DebitBalance(amountOfTransfer))
                {
                    return true;
                }
                else
                {
                    originatingCustomer.DebitBalance(amountOfTransfer);
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
