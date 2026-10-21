using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Finance_Tracker
{
    public class Authentication
    {
        private List<User> users = new List<User>();

        public Authentication()
        {

        }


        public bool SignUp(string userName, string password, string confirmPassword)
        { 
            if (string.IsNullOrWhiteSpace(userName)) return false;
            if (string.IsNullOrWhiteSpace(password)) return false;
            if (password != confirmPassword) return false;

            string name = userName.Trim();

            User CurrentUser = new User("Bob", "password");

            if (LoadUsers(CurrentUser)) {
                SaveUsers(CurrentUser);
            }
            

            return true;
        }

        public bool LogIn(User user)
        {
           if (users.Contains(user))
            {
                return true;
            }
            return false;
        }

        private void SaveUsers(User user)
        {
            users.Add(user);
        }

        private bool LoadUsers(User user)
        {
            if (!users.Contains(user))
            {
                return false;
            }
            return true;
        }

        private bool VerifyPassword(string password, User user)
        {
            if (password == user.Password)
            {
                return true;
            }
            return false;
        }

    }
}