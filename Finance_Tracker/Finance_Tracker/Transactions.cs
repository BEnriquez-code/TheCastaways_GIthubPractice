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
            float amount = 0.0f;
            if (account != null)

            {
                amount = CIO.PromptForFloat("Enter how much you would like to withdraw (between 0 and 1000 (inclusive)): ", 0.0f, 1000.0f);
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
            float amount = 0.0f;
            if (account != null)
            {
                amount = CIO.PromptForFloat("Enter how much you would like to deposit (between 0 and 1000 (inclusive)): ", 0.0f, 1000.0f);
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

    }
}
