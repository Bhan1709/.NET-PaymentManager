using FluentValidation;

namespace PaymentManager.Application.DTOs;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}