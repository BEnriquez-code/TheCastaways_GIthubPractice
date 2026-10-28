using System.Security;
using System.Security.Principal;

namespace Finance_Tracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BootUpLoop();
        }

        private static void BootUpLoop() {
            var userDataService = new UserData();
            var auth = new Authentication(userDataService);
            User? user = null;
            

            string[] bootUpOptions = { "Signup", "Login" };
            bool exit = false;
            do {
                int option = CIO.PromptForMenuSelection(bootUpOptions, true);
                switch (option) {
                    case 1: PromptRegistration(auth, userDataService);
                        break;
                    case 2: user = PromptLogin(auth, userDataService);
                        break;
                    case 0: exit = true;
                        break;
                }

                if (user != null) {
                    Console.WriteLine($"Welcome, {user.UserName}");
                }

            } while (!exit);
        }

        private static void FinancialTrackerLoop() {
            bool logout = false;
            do {
                
            } while (!logout);
        }

        private static void PromptRegistration(Authentication auth, UserData userDataService)
        {
            string username = "";
            string password = "";
            bool isInvalidUsername = true;
            bool isInvalidPassword = true;
            do {
                username = CIO.PromptForString("Choose a username: ", false);

                isInvalidUsername = userDataService.UserExists(username);

                if (isInvalidUsername) {
                    Console.WriteLine("That username has been taken");
                }

            } while (isInvalidUsername);

            do {
                

                password = CIO.PromptForString("Choose a password: ", false);

                bool isValidChars = password.All(c => c >= 33 && c <= 126);

                if (!isValidChars)
                {
                    Console.WriteLine("Password cannot contain spaces. Use only alphanumerical characters and standard keyboard symbols (e.g., !, @, #, $, %, *, etc.).");
                    continue;
                }

                string passwordConfirm = CIO.PromptForString("Confirm Password: ", false);
                bool passwordsMatch = string.Equals(password, passwordConfirm);

                if (!passwordsMatch)
                {
                    Console.WriteLine("Passwords do not match");
                }

                isInvalidPassword = !isValidChars || !passwordsMatch;

                if (isInvalidPassword) 
                {
                    Console.WriteLine("Could not create new user. Please try again");
                }

            } while (isInvalidPassword);

            bool isUserCreated = auth.SignUp(username, password, password,0);

            Console.WriteLine($"User {username} create: {isUserCreated}");
        }

        private static User? PromptLogin(Authentication auth, UserData userDataService)
        {
            string username = "";
            bool usernameFound = false;

            do
            {
                username = CIO.PromptForString("Enter your Username: ", false);
                usernameFound = userDataService.UserExists(username);
                if (!usernameFound)
                {
                    Console.WriteLine($"User with name \"{username}\" not found");
                }
            } while (!usernameFound);

            User? user = null;

            do
            {
                string password = CIO.PromptForString("Enter your Password: ", false); ;

                user = auth.LogIn(username, password);

                if (user == null) {
                    Console.WriteLine("This password does not match to account");
                }

            } while (user == null);

            return user;
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
        private static void PromptAddGoal(User user)
        {
            string goal = CIO.PromptForString("What goal do you want to accomplish?: ", false);
            user.Goals.Add(goal);
            Console.WriteLine($"Added {goal} to your list of {user.Goals}");

        }

        private static void ViewGoals(List<string> goals) {
            CIO.PrintOutList(goals);
        }
    }

}
