using System;
using System.Collections.Generic;
using System.Text;

namespace Finance_Tracker
{
    internal class User
    {
        public User(string userName, string password) {
            UserName = userName;
            Password = password;
        }
        private int id;
        private string userName;
        private string password;

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
