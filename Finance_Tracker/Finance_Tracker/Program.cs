using System.Security.Principal;

namespace Finance_Tracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var userDataService = new UserData();
            var auth = new Authentication(userDataService);
            var accountManager = new AccountManager(userDataService);



            bool userRegistered = auth.SignUp("UnCheesey", "Password123", "Password123");

            User? currentUser = auth.LogIn("UnCheesey", "Password123");
            Console.WriteLine($"Welcome, {currentUser?.UserName}");
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
