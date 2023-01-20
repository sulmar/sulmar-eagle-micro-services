using Core.Domain;

namespace Auth.Domain;

public interface IUserIdentityRepository : IEntityRepository<UserIdentity>
{
    UserIdentity GetByUsername(string username);
}


