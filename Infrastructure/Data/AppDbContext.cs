using Microsoft.EntityFrameworkCore;
using PaymentManager.Domain.Entities;

namespace PaymentManager.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Payment> Payments => Set<Payment>();
}