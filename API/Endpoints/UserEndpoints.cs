using FluentValidation;
using PaymentManager.Application.DTOs;
using PaymentManager.Application.Services;

namespace PaymentManager.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapPost("/users", async (
            CreateUserDto dto,
            IUserService service,
            IValidator<CreateUserDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors);

            var user = await service.CreateUserAsync(dto);

            return Results.Created($"/users/{user.Id}", user);
        });

        app.MapGet("/users", async (IUserService service) =>
        {
            var users = await service.GetUsersAsync();
            return Results.Ok(users);
        });
    }
}