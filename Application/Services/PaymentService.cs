using Microsoft.EntityFrameworkCore;
using PaymentManager.Application.DTOs;
using PaymentManager.Domain.Entities;
using PaymentManager.Infrastructure.Data;

namespace PaymentManager.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _db;

    public PaymentService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Payment> CreatePaymentAsync(CreatePaymentDto dto)
    {
        var payment = new Payment
        {
            Amount = dto.Amount,
            Currency = dto.Currency,
            UserId = dto.UserId,
            Status = "Pending"
        };

        _db.Payments.Add(payment);
        await _db.SaveChangesAsync();

        return payment;
    }

    public async Task<List<Payment>> GetPaymentsAsync()
    {
        return await _db.Payments
            .Include(p => p.User)
            .ToListAsync();
    }
}