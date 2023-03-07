namespace BankSystem.Models
{
    public enum AccountType
    {
        Current = 'C',
        Savings = 'S'
    }

    public sealed class BankAccount
    {
        public string Id { get; set; } = string.Empty;

        public AccountType AccountType { get; set; }

        public string AccountNo { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; } 
    }
}
