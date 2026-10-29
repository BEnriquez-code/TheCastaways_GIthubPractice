using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Finance_Tracker
{
    public class Manager
    {
        private readonly UserData _userData;

        public Manager(UserData userData) {
            _userData = userData;
        }

        public bool AddAccount(User user, string accountName, string pin, decimal initialBalance = 100m) {
            int accountNumber = new Random().Next(100000, 9999999);
            Account newAccount = new Account(accountName, accountNumber, pin, initialBalance);
            user.Accounts.Add(newAccount);

            return SaveUserChanges(user);
        }

        public void AddGoal(User user, string goal) {
            user.Goals.Add(goal);
            _userData.SaveGoals(user);
        }

        public bool RemoveAccount(User user, string accountName) {
            JsonArray json = _userData.ReadUsers();
            JsonNode? userNode = _userData.FindUser(json, user.UserName);

            if (userNode == null) return false;

            JsonArray? accounts = userNode["Accounts"] as JsonArray;
            if (accounts == null) return false;

            JsonNode? accountNode = _userData.FindAccount(accounts, accountName);
            if (accountNode == null) return false;

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

            if (account.Pin != currentPin) return false;
            if (newPin == currentPin) return false;

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
           if (newName.Length == 0) return false;

           Account? account = user.Accounts.FirstOrDefault(a =>
               string.Equals(a.AccountName, oldAccountName, StringComparison.OrdinalIgnoreCase));
           if (account == null) return false;

            bool nameTaken = user.Accounts.Any(a =>
                 !ReferenceEquals(a, account) &&
                 string.Equals(a.AccountName, newName, StringComparison.OrdinalIgnoreCase));
            if (nameTaken) return false;

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
            
            if (userNode == null) return false;

            int i = json.IndexOf(userNode);
            json[i] = JsonSerializer.SerializeToNode(user);
            _userData.WriteUsers(json);

            return true;
        }
    }
}
