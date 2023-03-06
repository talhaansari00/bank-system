using BankSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Pages.Users
{
    public class SetupModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SetupModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string ReturnUrl { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.PhoneNumber = Input.PhoneNumber;
            user.DateOfBirth = Input.DateOfBirth;
            user.PAN = Input.PAN;
            user.Address = Input.Address;

            await _userManager.UpdateAsync(user);

            return Redirect("/");
        }


        public sealed class InputModel
        {
            [Required]
            [Display(Name ="First Name")]
            public string FirstName { get; set; } = string.Empty;

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; } = string.Empty;

            [Required]
            [Display(Name = "Phone Number")]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; } = string.Empty;

            [Required]
            [Display(Name = "Date of Birth")]
            [DataType(DataType.Date)]
            public DateOnly DateOfBirth { get; set; }

            public char Gender { get; set; }

            public string PAN { get; set; } = string.Empty;

            public string AadharNo { get; set; } = string.Empty;

            public string Address { get; set; } = string.Empty;

            public string City { get; set; } = string.Empty;

            public string State { get; set; } = string.Empty;
        }
    }

}
