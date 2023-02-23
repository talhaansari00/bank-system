using Microsoft.AspNetCore.Identity;

namespace BankSystem.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string CustomerId { get; set; }  = string.Empty;
}