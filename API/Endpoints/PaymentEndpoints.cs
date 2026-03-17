using PaymentManager.Application.DTOs;
using PaymentManager.Application.Services;

namespace PaymentManager.API.Endpoints;

public static class PaymentEndpoints
{
    public static void MapPaymentEndpoints(this WebApplication app)
    {
        app.MapPost("/payments", async (CreatePaymentDto dto, IPaymentService service) =>
        {
            var payment = await service.CreatePaymentAsync(dto);
            return Results.Created($"/payments/{payment.Id}", payment);
        });

        app.MapGet("/payments", async (IPaymentService service) =>
        {
            var payments = await service.GetPaymentsAsync();
            return Results.Ok(payments);
        });
    }
}