using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankSite.Models.DbTables
{
    public class AccountType
    {
        [Key]
        public int AccountTypeId { get; set; }

        [ForeignKey("Account")]
        public int AccountId { get; set; }

        public Account Account { get; set; }
    }
}
