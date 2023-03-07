using BankSystem.Data;
using BankSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace BankSystem.Pages.Transactions
{
    public class DepositModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public DepositModel(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
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

            var account = await _dbContext.Accounts.Where(b => b.UserId == user.Id).FirstOrDefaultAsync();

            var transaction = new Transaction()
            {
                AccountId = account.Id,
                Amount = Input.Amount,
                Type = TransactionType.Credit,
            };

            await _dbContext.Transactions.AddAsync(transaction);

            await _dbContext.SaveChangesAsync();

            return Redirect("Index");
        }

        public class InputModel
        {
            public decimal Amount { get; set; }
        }
    }
}
