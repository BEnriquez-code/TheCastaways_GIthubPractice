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
            User? user;
            

            string[] bootUpOptions = { "Signup", "Login" };
            bool exit = false;
            do {
                user = null;

                int option = CIO.PromptForMenuSelection(bootUpOptions, true);
                switch (option) {
                    case 1: 
                        {
                            int age = PromptAge();
                            if (age >= 16)
                            {
                                PromptRegistration(age, auth, userDataService);
                            }
                            else
                            {
                                Console.WriteLine("You are not old enough to register.");
                            }
                        };
                        break;
                    case 2: user = PromptLogin(auth, userDataService);
                        break;
                    case 0: exit = true;
                        break;
                }

                if (user != null) {
                    FinancialTrackerLoop(user);
                }

            bool userRegistered = auth.SignUp("UnCheesey", "Password123", "Password123");

        private static void FinancialTrackerLoop(User user) {
            string[] options = ["Create New Account", "View Accounts", "View Goals", "Set New Goal"];
            bool logout = false;
            do {
                Console.WriteLine($"\n\nWelcome, {user.UserName}\n\n");

                int option = CIO.PromptForMenuSelection(options, true);

                switch (option)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 0:
                        logout = true;
                        break;
                }

            } while (!logout);
        }

        private static int PromptAge() {
            return CIO.PromptForInt("Insert your age: ", 1, int.MaxValue);
        }

        private static void PromptRegistration(int age, Authentication auth, UserData userDataService)
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

            bool isUserCreated = auth.SignUp(username, password, age);

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

           int choice = CIO.PromptForMenuSelection(["View Full Transaction History", "View Income", "View Expenses"], false);

            if (account.TransactionHistory == null)
            {
                account.TransactionHistory = new List<string>();
            }

            switch (choice)
            {
                case 1:
                    Transactions.ViewAccountTransactionHistory(account);
                    break;
                case 2:
                    Transactions.ViewAccountTransactionHistory(account, true);
                    break;
                case 3:
                    Transactions.ViewAccountTransactionHistory(account, false);
                    break;
                default:
                    return;
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
