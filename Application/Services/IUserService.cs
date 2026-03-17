using PaymentManager.Application.DTOs;
using PaymentManager.Domain.Entities;

namespace PaymentManager.Application.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(CreateUserDto dto);

    Task<List<User>> GetUsersAsync();
}