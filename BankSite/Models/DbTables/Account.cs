﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankSite.Models.DbTables
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [ForeignKey("AccountType")]
        public int AccountTypeId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Account Type")]
        public AccountType AccountType { get; set; }

        [Column(TypeName = "money")]
        public decimal Balance { get; set; }
    }

    public class AccountCreateViewModel
    {
        public List<AccountType> AllAccountTypes { get; set; }

        public int ChosenAccountTypeId { get; set; }
    }

    public class AccountIndexViewModel
    {
        public int AccountId { get; set; }

        public string AccountType { get; set; }

        public decimal Balance { get; set; }
    }

    public class AccountDepositViewModel
    {
        public Account Account { get; set; }

        
        [Range(0.01, double.MaxValue, ErrorMessage = "Deposit amount must be greater than $0")]
        [DataType(DataType.Currency)]
        [Display(Name = "Deposit Amount")]
        public decimal DepositAmount { get; set; }
    }
}
