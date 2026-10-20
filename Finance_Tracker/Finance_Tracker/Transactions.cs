using System;
using System.Collections.Generic;
using System.Text;

namespace Finance_Tracker
{
    internal static class Transactions
    {
        public static void Withdraw(Account account)
        {
            throw new NotImplementedException();
            // Ask For Amount And Validate
            int amount = 0;

            account.Balance -= amount;

        }

        public static void Deposit(Account account)
        {
            throw new NotImplementedException();
            // Ask For Amount
            int amount = 0;

            account.Balance += amount;
        }

        public static void ViewTransactionHistory()
        {
            throw new NotImplementedException();
        }

    }
}
