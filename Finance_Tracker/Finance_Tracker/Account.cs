using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Finance_Tracker
{
    internal class Account
    {
        public Account(string fName, string lName, int accountNum, string password, uint pin = 0000, float balance = 0.0f) {
            this.FirstName = fName;
            this.LastName = lName;
            this.AccountNumber = accountNum;
            this.password = password;
            this.pin = pin;
            this.balance = balance;
        }
        private float balance;
        private int accountNumber;
        private string firstName;
        private string lastName;
        private uint pin;
        private string password;


        #region Getter/Setters
        public float Balance { get; set; }
        public int AccountNumber { get; set; }
        public string FirstName { get;
            set{
                if (value != null || !value.IsWhiteSpace()) {
                    field = value!;
                }
            }
                 }
        public string LastName { get;
            set {
                if (value != null || !value.IsWhiteSpace())
                {
                    field = value!;
                }
            }
                 }
        public uint Pin { get; set; }
        public string Password { get; set; }
        #endregion

    }
}
