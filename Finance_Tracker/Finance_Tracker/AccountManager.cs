using System;
using System.Collections.Generic;
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
            int accountNumber = new Random().Next(100000, 9999999);
            Account newAccount = new Account(accountName, accountNumber, pin, initialBalance);
            user.Accounts.Add(newAccount);

            return SaveUserChanges(user);
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
