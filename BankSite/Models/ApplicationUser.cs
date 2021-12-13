using BankSite.Models.DbTables;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankSite.Models
{
    /// <summary>
    /// An extension of IdentityUser that stores first and last name separately.
    /// Also stores a list of Accounts that the user has created.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(75)")]
        public string LastName { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
