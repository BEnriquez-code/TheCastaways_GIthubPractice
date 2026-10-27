using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Finance_Tracker
{
    public class UserData
    {
        private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "users.json");
        public JsonArray ReadUsers() {
            if (!File.Exists(_filePath)) return new JsonArray();

            try
            {
                return JsonNode.Parse(File.ReadAllText(_filePath)) as JsonArray ?? new JsonArray();
            }
            catch (JsonException)
            {
                return new JsonArray();
            }
        }

        public void WriteUsers(JsonArray json) {
            File.WriteAllText(_filePath, json.ToJsonString());
        }

        public JsonNode? FindUser(JsonArray json, string userName)
        {
            return json.FirstOrDefault(u =>
                string.Equals(u?["UserName"]?.GetValue<string>(), userName,
                    StringComparison.OrdinalIgnoreCase));
        }

        public bool UserExists(string userName)
        {
            return FindUser(ReadUsers(), userName) != null;
        }

        public long GenerateUniqueId(JsonArray json)
        {
            long id;

            do
            {
                byte[] bytes = Guid.NewGuid().ToByteArray();
                id = BitConverter.ToInt64(bytes, 0) & long.MaxValue;
                if (id == 0) id = 1;

            } while (json.Any(u => u?["Id"]?.GetValue<long>() == id));

            return 0;
        }
    }
}
