using System;
using System.Collections.Generic;
using System.Text;


namespace Finance_Tracker
{
    internal static class Transactions
    {
        public static void Withdraw(Account account)
        {
            // Ask For Amount And Validate
            decimal amount = 0.00m;
            if (account != null)

            {
                amount = CIO.PromptForDecimal("Enter how much you would like to withdraw (between 0 and 1000 (inclusive)): ", 0.00m, 1000.00m);
                if (account.Balance - amount <= 0 || account.Balance < 0)
                {
                    Console.WriteLine("Your balance is too low to withdraw this amount!");
                    return;
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

            account!.Balance -= amount;
            account!.TransactionHistory.Add($"-{amount}");
            Console.WriteLine($"Withdrew ${amount} from account!");


        }

        public static void Deposit(Account account)
        {
            // Ask For Amount
            decimal amount = 0.00m;
            if (account != null)
            {
                amount = CIO.PromptForDecimal("Enter how much you would like to deposit (between 0 and 1000 (inclusive)): ", 0.00m, 1000.00m);
            }
            else
            {
                throw new ArgumentNullException();
            }

            account!.Balance += amount;
            account!.TransactionHistory.Add($"+{amount}");

            Console.WriteLine($"Added ${amount} to account!");
        }

        public static void ViewAccountTransactionHistory(Account account)
        {
            int size = account.TransactionHistory.Count;
            for (int i = size - 1; i >= 0 ; i--)
            {
                Console.WriteLine($"{account.TransactionHistory[i]}");
            }
        }

        public static void ViewAccountTransactionHistory(Account account, bool type) // True = Income, False = Expenses
        {
            foreach(string tranH in account.TransactionHistory)
            {
                if (tranH.Contains("+") && type)
                {
                    Console.WriteLine(tranH);
                } if (tranH.Contains("-") && !type)
                {
                    Console.WriteLine(tranH);
                }
            }
        }

    }
}
