using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        [Required]
        public double Balance { get; set; }

        [Required]
        [MaxLength(40)]
        public string AccountType { get; set; }

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        [Required]
        public Guid CustomerId { get; set; }

        public Account()
        {
            Balance = 0;
            AccountType = "Default";
            Id = Guid.NewGuid();
        }
        public Account(double bal, string atype, Guid custId)
        {
            Balance = bal;
            AccountType = atype;
            Id = Guid.NewGuid();
            CustomerId = custId;
        }  

        public override string ToString()
        {
            return $"{AccountType} account; Current balance = {Balance.ToString("C")}";

        }

    }
}
