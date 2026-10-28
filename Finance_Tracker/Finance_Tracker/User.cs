using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Finance_Tracker
{
    public class User
    {
        public long Id { get; set; }
        public int Age
        {
            get;
            set
            {
                if (value >= 15)
                {
                    field = value;
                }
            }
        }
        public string UserName
        {
            get;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    field = value;
                }
            }
        }
        public string Password
        {
            get;
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.All(c => c >= 33 && c <= 126))
                {
                    field = value;
                }
            }
        }
        public List<Account> Accounts { get; set; } = new List<Account>();

        public User() { }

        public User(long id, string name, string password, int age) {
            Id = id;
            UserName = name;
            Password = password;
            Age = age;
        }

       

        
    }
}
