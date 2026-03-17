using FluentValidation;
using PaymentManager.Application.DTOs;
using PaymentManager.Application.Services;

namespace PaymentManager.API.Endpoints;


public static class PaymentEndpoints
{
    public static void MapPaymentEndpoints(this WebApplication app)
    {
        app.MapPost("/payments", async (CreatePaymentDto dto, IPaymentService service, IValidator<CreatePaymentDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }
            
            var payment = await service.CreatePaymentAsync(dto);
            return Results.Created($"/payments/{payment.Id}", payment);
        });

        app.MapGet("/payments", async (
            string? status,
            int page,
            int pageSize,
            IPaymentService service) =>
        {
            var payments = await service.GetPaymentsAsync(status, page, pageSize);
            return Results.Ok(payments);
        });

        app.MapPut("/payments/{id}/status", async (
            int id,
            string status,
            IPaymentService service) =>
        {
            var updated = await service.UpdatePaymentStatusAsync(id, status);

            if (!updated)
                return Results.BadRequest("Invalid payment ID or status");

            return Results.Ok();
        });
    }
}