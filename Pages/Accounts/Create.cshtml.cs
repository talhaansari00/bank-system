using BankSystem.Data;
using BankSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankSystem.Pages.Accounts
{
    public class CreateModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public CreateModel(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
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

            var account = new BankAccount()
            {
                AccountNo = GenerateAccountNumber(),
                AccountType = Input.AccountType,
                UserId = user.Id,
            };

            await _dbContext.Accounts.AddAsync(account);

            var transaction = new Transaction()
            {
                AccountId = account.Id,
                Amount = Input.InitialDepositAmount,
                Type = TransactionType.Credit,
            };

            await _dbContext.Transactions.AddAsync(transaction);

            await _dbContext.SaveChangesAsync();

            return Redirect("Index");
        }

        public sealed class InputModel
        {
            public AccountType AccountType { get; set; }

            public decimal InitialDepositAmount { get; set; }
        }

        private static string GenerateAccountNumber()
        {
            return "ABC" + Guid.NewGuid().ToString("N");
        }
    }
}
