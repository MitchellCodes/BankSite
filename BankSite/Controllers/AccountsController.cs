using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankSite.Data;
using BankSite.Models.DbTables;
using Microsoft.AspNetCore.Identity;
using BankSite.Models;

namespace BankSite.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            IQueryable<AccountIndexViewModel> applicationDbContext =
                from account in _context.Accounts
                join accountType in _context.AccountTypes on account.AccountTypeId equals accountType.AccountTypeId
                where account.UserId == _userManager.GetUserId(User)
                select new AccountIndexViewModel
                {
                    AccountId = account.AccountId,
                    AccountType = accountType.TypeName,
                    Balance = account.Balance
                };
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.AccountType)
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            AccountCreateViewModel viewModel = new();
            viewModel.AllAccountTypes = _context.AccountTypes.ToList();
            return View(viewModel);
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AccountCreateViewModel account)
        {
            if (ModelState.IsValid)
            {
                Account newAccount = new()
                {
                    UserId = _userManager.GetUserId(User),
                    AccountTypeId = account.ChosenAccountTypeId
                };

                _context.Add(newAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            account.AllAccountTypes = _context.AccountTypes.ToList();
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.AccountType)
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }

        [HttpGet]
        public async Task<IActionResult> Deposit(int? id)
        {
            Account accountToEdit = await _context.Accounts.FirstAsync(a => a.AccountId == id);
            AccountDepositViewModel viewModel = new()
            {
                AccountId = accountToEdit.AccountId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(AccountDepositViewModel accountWithDeposit)
        {
            if (ModelState.IsValid)
            {
                Account account = await _context.Accounts.FindAsync(accountWithDeposit.AccountId);
                account.Balance += accountWithDeposit.DepositAmount;
                _context.Update(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountWithDeposit);
        }

        [HttpGet]
        public async Task<IActionResult> Withdraw(int? id)
        {
            Account accountToEdit = await _context.Accounts.FirstAsync(a => a.AccountId == id);
            AccountWithdrawViewModel viewModel = new()
            {
                AccountId = accountToEdit.AccountId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(AccountWithdrawViewModel accountWithWithdrawal)
        {
            if (ModelState.IsValid)
            {
                Account account = await _context.Accounts.FindAsync(accountWithWithdrawal.AccountId);
                account.Balance -= accountWithWithdrawal.WithdrawAmount;
                _context.Update(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accountWithWithdrawal);
        }
    }
}
