using Api.DTOs.Requests;
using FluentValidation;

namespace Api.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 10 characters long.");

            RuleFor(x => x.Nickname)
                .NotEmpty().WithMessage("Nickname is required.")
                .MinimumLength(10).WithMessage("Nickname must be at least 10 characters long.");

            RuleFor(x => x.Age)
                .NotEmpty().WithMessage("Age is required.")
                .GreaterThanOrEqualTo(18).WithMessage("Age must be at least 18.");
        }
    }
}
