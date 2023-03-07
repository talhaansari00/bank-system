namespace BankSystem.Models
{
    public enum TransactionType
    {
        Credit = 'C',
        Debit = 'D'
    }

    public sealed class Transaction
    {
        public Guid Id { get; set; }

        public TransactionType Type { get; set; }

        public DateTime OccurredOn { get; set; } = DateTime.Now;

        public decimal Amount { get; set; }

        public string AccountId { get; set; } = string.Empty;
        
        public string Remark { get; set; } = string.Empty;
    }
}
