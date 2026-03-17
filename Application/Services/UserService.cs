using Microsoft.EntityFrameworkCore;
using PaymentManager.Application.DTOs;
using PaymentManager.Domain.Entities;
using PaymentManager.Infrastructure.Data;

namespace PaymentManager.Application.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly ILogger<UserService> _logger;

    public UserService(AppDbContext db, ILogger<UserService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<User> CreateUserAsync(CreateUserDto dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        _logger.LogInformation("Created user {UserId}", user.Id);

        return user;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _db.Users.ToListAsync();
    }
}