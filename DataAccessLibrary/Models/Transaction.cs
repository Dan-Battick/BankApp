using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary
{
    public class Transaction
    {
        public Guid Id { get; set; }
        
        [Required]
        public double TransactionAmount { get; set; }
        
        [Required]
        public DateTime TransactionDate { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string TransactionType { get; set; }
        
        [Required]
        public Guid AccountId { get; set; }

        public Transaction()
        {
            TransactionAmount = 0;
            TransactionDate = DateTime.Now;
            TransactionType = "Default";
            Id = Guid.NewGuid();
        }

        public Transaction(double amt, string ttype)
        {
            TransactionAmount = amt;
            TransactionDate = DateTime.Now;
            TransactionType = ttype;
            Id = Guid.NewGuid(); 
        }


        public override string ToString()
        {
            return $"{TransactionType}: ${TransactionAmount} on {TransactionDate} done on account {AccountId}";
        }
    }
}
