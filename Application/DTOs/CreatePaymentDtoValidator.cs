using FluentValidation;

namespace PaymentManager.Application.DTOs;

public class CreatePaymentDtoValidator : AbstractValidator<CreatePaymentDto>
{
    public CreatePaymentDtoValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

        RuleFor(x => x.Currency)
            .NotEmpty()
            .Length(3).WithMessage("Currency must be 3 characters");

        RuleFor(x => x.UserId)
            .GreaterThan(0);
    }
}