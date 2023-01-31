﻿using FluentValidation;

namespace Auth.Api.Extensions;

public static class RuleBuilderExtensions
{
    public static void Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 10)
    {
        ruleBuilder
            .MinimumLength(minimumLength)
            .WithMessage($"Minimum length must be {minimumLength}")
            .Matches("[a-z]")
            .WithMessage("You need to have at least one lowercase letter")
            .Matches("[A-Z]")
            .WithMessage("You need to have at least one uppercase letter")
            .Matches("[0-9]")
            .WithMessage("You need to have at least one digit")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("You need to have at least one special character");
    }
}
