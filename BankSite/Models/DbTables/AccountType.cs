using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankSite.Models.DbTables
{
    /// <summary>
    /// A type of bank account.
    /// Each account can only have one type.
    /// </summary>
    public class AccountType
    {
        [Key]
        public int AccountTypeId { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string TypeName { get; set; }

        public float InterestRate { get; set; }
    }
}
