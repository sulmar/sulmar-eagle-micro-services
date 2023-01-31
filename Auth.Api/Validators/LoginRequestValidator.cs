using Auth.Api.Extensions;
using Auth.Api.Models;
using FluentValidation;

namespace Auth.Api.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(p=>p.Login).NotEmpty().MinimumLength(10);
        RuleFor(p => p.Password).StrongPassword();
    }
}
