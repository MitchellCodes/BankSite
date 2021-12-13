using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankSite.Models.DbTables
{
    /// <summary>
    /// A bank account that can be created by an ApplicationUser.
    /// Users can have many accounts of different types.
    /// </summary>
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

    /// <summary>
    /// A view model to be used on the Accounts/Create view.
    /// </summary>
    public class AccountCreateViewModel
    {
        public List<AccountType> AllAccountTypes { get; set; }

        public int ChosenAccountTypeId { get; set; }
    }

    /// <summary>
    /// A view model to be used on the Accounts/Index view.
    /// </summary>
    public class AccountIndexViewModel
    {
        public int AccountId { get; set; }

        [Display(Name = "Account Type")]
        public string AccountType { get; set; }

        public decimal Balance { get; set; }
    }

    /// <summary>
    /// A view model to be used on the Accounts/Deposit view.
    /// </summary>
    public class AccountDepositViewModel
    {
        public int AccountId { get; set; }

        
        [Range(0.01, double.MaxValue, ErrorMessage = "Deposit amount must be greater than $0")]
        [DataType(DataType.Currency)]
        [Display(Name = "Deposit Amount")]
        public decimal DepositAmount { get; set; }
    }

    /// <summary>
    /// A view model to be used on the Accounts/Withdraw view.
    /// </summary>
    public class AccountWithdrawViewModel
    {
        public int AccountId { get; set; }


        [Range(0.01, double.MaxValue, ErrorMessage = "Withdraw amount must be greater than $0")]
        [DataType(DataType.Currency)]
        [Display(Name = "Withdraw Amount")]
        public decimal WithdrawAmount { get; set; }
    }
}
