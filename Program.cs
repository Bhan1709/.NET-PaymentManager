using Microsoft.EntityFrameworkCore;
using PaymentManager.Infrastructure.Data;
using PaymentManager.Application.Services;
using PaymentManager.API.Endpoints;
using FluentValidation;
using PaymentManager.Application.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePaymentDtoValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Payment Manager API Running");
app.MapPaymentEndpoints();

app.Run();