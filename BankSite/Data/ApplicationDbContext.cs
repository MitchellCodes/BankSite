using BankSite.Models;
using BankSite.Models.DbTables;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankSite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<AccountType> AccountTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Account>().Property(a => a.Balance).HasDefaultValue(0);
            builder.Entity<Account>().HasOne<ApplicationUser>(a => a.ApplicationUser).WithMany().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AccountType>().HasData(
                new AccountType { AccountTypeId = 1, TypeName = "Checking", InterestRate = .03f},
                new AccountType { AccountTypeId = 2, TypeName = "Savings", InterestRate = .06f},
                new AccountType { AccountTypeId = 3, TypeName = "Money Market", InterestRate = .09f},
                new AccountType { AccountTypeId = 4, TypeName = "Certificate of Deposit", InterestRate = .5f});
        }
    }
}
