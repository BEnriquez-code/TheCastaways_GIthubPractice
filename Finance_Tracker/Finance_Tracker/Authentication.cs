using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Finance_Tracker
{
    public class Authentication
    {
        private readonly UserData _userData;

        public Authentication(UserData userData)
        {
            _userData = userData;
        }

        public bool SignUp(string userName, string password, string confirmPassword)
        { 
            if (string.IsNullOrWhiteSpace(userName)) return false;
            if (string.IsNullOrWhiteSpace(password)) return false;
            if (password != confirmPassword) return false;

            string name = userName.Trim();

            JsonArray json = _userData.ReadUsers();

            if (_userData.FindUser(json, name) != null) return false;

            long uniqueId = _userData.GenerateUniqueId(json);   
            User newUser = new User(uniqueId, name, password);

            json.Add(JsonSerializer.SerializeToNode(newUser));
            _userData.WriteUsers(json);

            return true;
        }

        public User? LogIn(string userName, string password)
        {
            JsonArray json = _userData.ReadUsers();
            JsonNode? user = _userData.FindUser(json, userName.Trim());

            if (user == null) return null;
            if (user["Password"]?.GetValue<string>() != password) return null;

            User loggedInUser = new User {
                Id = user["Id"]?.GetValue<long>() ?? 0,
                UserName = user["UserName"]?.GetValue<string>() ?? string.Empty,
                Password = user["Password"]?.GetValue<string>() ?? string.Empty,
                Age = user["Age"]?.GetValue<int>() ?? 0
            };

            if (user["Accounts"] is JsonArray accountsArray) {
                loggedInUser.Accounts = JsonSerializer.Deserialize<List<Account>>(accountsArray.ToJsonString()) ?? new List<Account>();
            }
            return loggedInUser;
        }

        
    }
}