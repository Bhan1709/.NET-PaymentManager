using Microsoft.EntityFrameworkCore;
using PaymentManager.Application.DTOs;
using PaymentManager.Domain.Entities;
using PaymentManager.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace PaymentManager.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _db;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(AppDbContext db, ILogger<PaymentService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<Payment> CreatePaymentAsync(CreatePaymentDto dto)
    {
        var payment = new Payment
        {
            Amount = dto.Amount,
            Currency = dto.Currency,
            UserId = dto.UserId,
            Status = PaymentStatus.Pending
        };

        _db.Payments.Add(payment);
        _logger.LogInformation("Creating payment for UserId: {UserId}, Amount: {Amount}", dto.UserId, dto.Amount);
        await _db.SaveChangesAsync();
        return payment;
    }

    public async Task<List<Payment>> GetPaymentsAsync()
    {
        return await _db.Payments
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task<bool> UpdatePaymentStatusAsync(int id, string status)
    {
        var payment = await _db.Payments.FindAsync(id);

        if (payment == null)
            return false;

        if (!Enum.TryParse<PaymentStatus>(status, true, out var parsedStatus))
            return false;

        payment.Status = parsedStatus;

        _logger.LogInformation("Updating payment {PaymentId} to status {Status}", id, status);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task<List<Payment>> GetPaymentsAsync(string? status, int page, int pageSize)
    {
        var query = _db.Payments.Include(p => p.User).AsQueryable();

        if (!string.IsNullOrEmpty(status) &&
            Enum.TryParse<PaymentStatus>(status, true, out var parsedStatus))
        {
            query = query.Where(p => p.Status == parsedStatus);
        }

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}