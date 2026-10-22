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
        private string filePath = Path.Combine(AppContext.BaseDirectory, "users.json");

        public Authentication(){}

        public bool SignUp(string userName, string password, string confirmPassword)
        { 
            if (string.IsNullOrWhiteSpace(userName)) return false;
            if (string.IsNullOrWhiteSpace(password)) return false;
            if (password != confirmPassword) return false;

            string name = userName.Trim();

            JsonArray json = ReadUsers();

            if (FindUser(json, name) != null) return false;

            json.Add(JsonSerializer.SerializeToNode(new User(name, password)));
            WriteUser(json);

            return true;
        }

        public bool LogIn(string userName, string password)
        {
            JsonNode? user = FindUser(ReadUsers(), userName.Trim());
            return user != null && user["Password"]?.GetValue<string>() == password;
        }

        private JsonNode? FindUser(JsonArray json, string userName) { 
            return json.FirstOrDefault(u =>
                string.Equals(u?["UserName"]?.GetValue<string>(), userName,
                    StringComparison.OrdinalIgnoreCase));
        }



        private JsonArray ReadUsers()
        {
            if (!File.Exists(filePath)) return new JsonArray();

            try
            {
                return JsonNode.Parse(File.ReadAllText(filePath)) as JsonArray
                    ?? new JsonArray();
            }
            catch (JsonException) {
                return new JsonArray();
            }   
        }

        private void WriteUser(JsonArray json)
        {
            File.WriteAllText(filePath, json.ToJsonString());
        }

    }
}