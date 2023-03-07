using BankSystem.Data;
using BankSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Pages.Transactions
{
    public class TransferModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public TransferModel(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            var source = await _dbContext.Accounts.Where(b => b.UserId == user.Id).FirstOrDefaultAsync();

            var debitTrn = new Transaction()
            {
                AccountId = source.Id,
                Amount = Input.Amount,
                Type = TransactionType.Debit,
            };


            var destination = await _dbContext.Accounts.Where(b => b.UserId == user.Id).FirstOrDefaultAsync();

            var creditTrn = new Transaction()
            {
                AccountId = destination.Id,
                Amount = Input.Amount,
                Type = TransactionType.Credit,
            };

            await _dbContext.Transactions.AddAsync(debitTrn);

            await _dbContext.SaveChangesAsync();

            return Redirect("Index");
        }

        public class InputModel
        {
            [Required]
            public decimal Amount { get; set; }


            [Required]
            public string AccountNo { get; set; } = string.Empty;
        }
    }
}
