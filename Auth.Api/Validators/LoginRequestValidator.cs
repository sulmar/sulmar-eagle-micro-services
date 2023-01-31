using Auth.Api.Extensions;
using Auth.Api.Models;
using FluentValidation;

namespace Auth.Api.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(p => p.Password).Password();
    }
}
