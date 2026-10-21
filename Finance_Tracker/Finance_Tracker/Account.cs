using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Finance_Tracker
{
    internal class Account
    {
        public Account() {
            
        }
        public Account(string accountName, int accountNum, string password, string pin = "0000", float balance = 0.0f) {
            this.AccountName = accountName;
            this.AccountNumber = accountNum;
            this.Password = password;
            this.Pin = pin;
            this.Balance = balance;
        }

        private float balance;
        private int accountNumber;
        private string accountName;
        private string pin;
        private string password;


        #region Getter/Setters
        public float Balance { get; set; }
        public int AccountNumber { get; set; }

        public string AccountName { get;
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
