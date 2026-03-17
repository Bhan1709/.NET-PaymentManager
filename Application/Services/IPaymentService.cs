using PaymentManager.Application.DTOs;
using PaymentManager.Domain.Entities;

namespace PaymentManager.Application.Services;

public interface IPaymentService
{
    Task<Payment> CreatePaymentAsync(CreatePaymentDto dto);

    Task<List<Payment>> GetPaymentsAsync();
}