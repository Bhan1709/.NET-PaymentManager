namespace PaymentManager.Application.DTOs;

public class CreatePaymentDto
{
    public decimal Amount { get; set; }

    public string Currency { get; set; } = "USD";

    public int UserId { get; set; }
}