namespace PaymentManager.Domain.Entities;

public class Payment
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public string Currency { get; set; } = "USD";

    public string Status { get; set; } = "Pending";

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}