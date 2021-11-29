using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mediator_cqrs_project.Models
{
    [Table("account")]
    public class Account
    {
        public Account(int id, string documentNumber, int accountType, string accountOwner, decimal accountBalance)
        {
            this.Id = id;
            this.DocumentNumber = documentNumber;
            this.AccountType = accountType;
            this.AccountOwner = accountOwner;
            this.AccountBalance = accountBalance;
        }

        public Account() { }

        [Key]
        public int Id { get; private set; }
        
        public string DocumentNumber { get; set; }
        
        public int AccountType { get; set; }
        
        public string AccountOwner { get; set; }

        public decimal AccountBalance { get; set; }
    }
}
