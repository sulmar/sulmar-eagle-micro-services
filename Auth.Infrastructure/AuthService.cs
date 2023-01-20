using Auth.Domain;
using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure;

public class AuthService : IAuthService
{
    private readonly IUserIdentityRepository userIdentityRepository;

    // dotnet add package Microsoft.Extensions.Identity.Core
    private readonly IPasswordHasher<UserIdentity> passwordHasher;

    public AuthService(IUserIdentityRepository userIdentityRepository, IPasswordHasher<UserIdentity> passwordHasher)
    {
        this.userIdentityRepository = userIdentityRepository;
        this.passwordHasher = passwordHasher;
    }

    public bool TryAuthorize(string username, string password, out UserIdentity user)
    {
        user = userIdentityRepository.GetByUsername(username);

        return user != null && passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password) == PasswordVerificationResult.Success;
    }
}
