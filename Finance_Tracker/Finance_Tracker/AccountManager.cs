using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Finance_Tracker
{
    public class AccountManager
    {
        private readonly UserData _userData;

        public AccountManager(UserData userData) {
            _userData = userData;
        }

        public bool AddAccount(User user, string accountName, string pin, decimal initialBalance = 100m) {
            if (string.IsNullOrWhiteSpace(accountName)) return false;
            string name = accountName.Trim();

            bool exist = user.Accounts.Any(a =>
                string.Equals(a.AccountName, name, StringComparison.OrdinalIgnoreCase));

            if (exist) {
                Console.Error.WriteLine("Cannot add new account that already exists, check spelliing errors");
                return false; 
            }

            int accountNumber = new Random().Next(100000, 9999999);
            Account newAccount = new Account(accountName, accountNumber, pin, initialBalance);
            user.Accounts.Add(newAccount);

            return SaveUserChanges(user);
        }

        public bool RemoveAccount(User user, string accountName) {
            JsonArray json = _userData.ReadUsers();
            JsonNode? userNode = _userData.FindUser(json, user.UserName);

            if (userNode == null) return false;

            JsonArray? accounts = userNode["Accounts"] as JsonArray;
            if (accounts == null) {
                Console.Error.WriteLine("Cannot remove Account because Accounts is nonexistent");
                return false; 
            }

            JsonNode? accountNode = _userData.FindAccount(accounts, accountName);
            if (accountNode == null) {
                Console.Error.WriteLine($"Cannot Find account with the name of {accountName}");
                return false; 
            }

            if (!accounts.Remove(accountNode)) return false;
            _userData.WriteUsers(json);

            user.Accounts.RemoveAll(a =>
                string.Equals(a.AccountName, accountName, StringComparison.OrdinalIgnoreCase));
            return true;
        }

        public bool ChangePin(User user, string accountName, string currentPin, string newPin) {
            if (string.IsNullOrWhiteSpace(newPin) || (newPin.Length >= 4 && newPin.Length <= 6)) return false;

            Account? account = user.Accounts.FirstOrDefault(a =>
                string.Equals(a.AccountName, accountName, StringComparison.OrdinalIgnoreCase));
            if (account == null) return false;

            if (account.Pin != currentPin) {
                Console.Error.WriteLine("Cannot change pin because the Accounts current pin does not match input");
                return false; 
            }
            if (newPin == currentPin) {
                Console.Error.WriteLine("Cannot change pin because (newPin) is the same as the input (current pin)");
                return false; 
            }

            string previousPin = account.Pin;
            account.Pin = newPin;

            if (!SaveUserChanges(user))
            {
                account.Pin = previousPin;
                return false;
            }
            return true;
        }

        public bool RenameAccount(User user, string oldAccountName, string newAccountName ) {
            string newName = newAccountName.Trim() ?? "";
            if (newName.Length <= 0) {
                Console.Error.WriteLine("Cannot rename account because new account name is less than or equal to 0");
                return false; 
            }

            Account? account = user.Accounts.FirstOrDefault(a =>
                string.Equals(a.AccountName, oldAccountName, StringComparison.OrdinalIgnoreCase));
            if (account == null) {
                Console.Error.WriteLine($"Cannot rename account because there is no account by the name of {oldAccountName}");
                return false; 
            }

            bool nameTaken = user.Accounts.Any(a =>
                    !ReferenceEquals(a, account) &&
                    string.Equals(a.AccountName, newName, StringComparison.OrdinalIgnoreCase));
            if (nameTaken) {
                Console.Error.WriteLine($"Cannot rename account to {newName} because it is taken by another account");
                return false; 
            }

            string previousName = account.AccountName;
            account.AccountName = newName;

            if (!SaveUserChanges(user)) {
                account.AccountName = previousName;
                return false;
            }
            return true;

        }

        private bool SaveUserChanges(User user) {
            JsonArray json = _userData.ReadUsers();
            JsonNode? userNode = _userData.FindUser(json, user.UserName);

            if (userNode == null) {
                Console.WriteLine($"Cannot save user changes, cannot find user by the name of {user.UserName} ");
                return false; 
            }

            int i = json.IndexOf(userNode);
            json[i] = JsonSerializer.SerializeToNode(user);
            _userData.WriteUsers(json);

            return true;
        }
    }
}
