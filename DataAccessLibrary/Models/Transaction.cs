using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary
{
    public class Transaction
    {
        public Guid Id { get; set; }
        //[Required]
        public double TransactionAmount { get; set; }
        //[Required]
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        //[Required]
        public Guid AccountId { get; set; }

        public Transaction(double amt, string type)
        {
            TransactionAmount = amt;
            TransactionDate = DateTime.Now;
            TransactionType = type;
            Id = Guid.NewGuid(); 
        }


        public override string ToString()
        {
            return $"{TransactionType}: ${TransactionAmount} on {TransactionDate}";
        }
    }
}
