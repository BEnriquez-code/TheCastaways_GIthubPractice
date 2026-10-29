using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;


namespace Finance_Tracker
{
    internal static class Transactions
    {
        public static bool Withdraw(Account account)
        {
            // Ask For Amount And Validate
            decimal amount = 0.00m;
            if (account != null)

            {
                amount = CIO.PromptForDecimal($"Enter how much you would like to withdraw (balance: {account.Balance}) : ", 0.00m,account.Balance);
                if (account.Balance - amount < 0 || account.Balance < 0)
                {
                    Console.WriteLine("Your balance is too low to withdraw this amount!");
                    ///the transaction didn't go through
                    return false;
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

            account!.Balance -= amount;
            account!.TransactionHistory.Add($"-{amount}");
            Console.WriteLine($"Withdrew ${amount} from account!");
            //the transaction DID went through
            return true;

        }

        public static void Deposit(Account account)
        {
            // Ask For Amount
            decimal amount = 0.00m;
            if (account != null)
            {
                amount = CIO.PromptForDecimal("Enter how much you would like to deposit: ", 0.00m,decimal.MaxValue);
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
            foreach (string tranH in account.TransactionHistory)
            {
                if (tranH.Contains("+") && type)
                {
                    Console.WriteLine(tranH);
                }
                if (tranH.Contains("-") && !type)
                {
                    Console.WriteLine(tranH);
                }
            }
        }
        public static void Transfer(Account sender, Account receiver) {
            Console.WriteLine("Select how much money to withdraw from your account to send\n");
            decimal moneyQuantity = sender.Balance;
            bool didItGoThrough = Withdraw(sender);
            decimal transactionAmount = moneyQuantity - sender.Balance;
            
            
            if (didItGoThrough) {
                receiver!.Balance += transactionAmount;
                receiver!.TransactionHistory.Add($"+{transactionAmount}");

                Console.WriteLine($"{receiver.AccountName} received ${transactionAmount} !\n");
                Console.WriteLine($"{receiver.AccountName}'s balance: ${receiver.Balance}");
            }
            else
            {
                Console.WriteLine($"The transaction couldn't be made");
            }
       
        
        
        }

    }
}
