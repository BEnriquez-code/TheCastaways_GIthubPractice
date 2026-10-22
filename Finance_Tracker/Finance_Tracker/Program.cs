using System.Security.Principal;

namespace Finance_Tracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Start Code for Depositing (NOT IMPLEMENTED)
            Account newAccount = new Account();
            newAccount.Balance = 2000;
            Deposit(newAccount, 2000);
            
            Deposit(newAccount);
            Withdraw(newAccount);
            ViewHistory(newAccount);   
        }

        private static void Withdraw(Account account) { 
            if (account.TransactionHistory == null)
            {
                account.TransactionHistory = new List<string>();
                Transactions.Withdraw(account);
            } else
            {
                Transactions.Withdraw(account);
            }
            Console.WriteLine($"Current balance is: ${account.Balance}");
        }
        private static void Deposit(Account account) {
            if (account.TransactionHistory == null)
            {
                account.TransactionHistory = new List<string>();
                Transactions.Deposit(account);
            }
            else
            {
                Transactions.Deposit(account);
            }

            Console.WriteLine($"Current balance is: ${account.Balance}");
        }

        private static void Deposit(Account account, int amount)
        {
            if (account.TransactionHistory == null)
            {
                account.TransactionHistory = new List<string>();
                account.TransactionHistory.Add($"+{amount}");
            }
            else
            {
                account.TransactionHistory.Add($"+{amount}");
            }

            Console.WriteLine($"Current balance is: ${account.Balance}");
        }

        private static void ViewHistory(Account account) {
            if (account.TransactionHistory == null)
            {
                account.TransactionHistory = new List<string>();
                Transactions.ViewAccountTransactionHistory(account);
            }
            else
            {
                Transactions.ViewAccountTransactionHistory(account);
            }

            Console.WriteLine($"Current balance is: ${account.Balance}");
        }
    }
}
