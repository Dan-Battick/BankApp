using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Customer
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string Name { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
        
        public Customer(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }
    }
}
