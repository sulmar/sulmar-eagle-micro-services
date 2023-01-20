using Auth.Domain;
using Bogus;
using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure;

public sealed class UserIdentityFaker : Faker<UserIdentity>
{
    public UserIdentityFaker(IPasswordHasher<UserIdentity> passwordHasher)
    {
        UseSeed(1);
        RuleFor(p => p.Id, f => f.IndexFaker);
        RuleFor(p => p.Username, f => f.Person.UserName);
        RuleFor(p => p.HashedPassword, (f, user) => passwordHasher.HashPassword(user, "123"));
        RuleFor(p => p.Email, f => f.Person.Email);
        RuleFor(p => p.Phone, f => f.Person.Phone);

    }
}
