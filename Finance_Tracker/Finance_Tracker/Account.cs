using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Finance_Tracker
{
    public class Account
    {
        public Account() {
            TransactionHistory = new List<string>();
        }
        public Account(string accountName, int accountNum, string pin = "0000", decimal balance = 0.00m) {
            this.AccountName = accountName;
            this.AccountNumber = accountNum;
            this.Pin = pin;
            this.Balance = balance;
            TransactionHistory = new List<string>();
        }

        #region Getter/Setters
        public decimal Balance { get; set; }
        public int AccountNumber { get; set; }

        public string AccountName { get;
            set {
                if (value != null || !value.IsWhiteSpace())
                {
                    field = value!;
                }
            }
                 }

        public string Pin { get; set; }

        public List<string> TransactionHistory { get; set; }
        #endregion

    }
}
