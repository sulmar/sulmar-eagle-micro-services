using Auth.Domain;

namespace Auth.Infrastructure;

public class InMemoryUserIdentityRepository : IUserIdentityRepository
{
    private readonly IDictionary<int, UserIdentity> _users = new Dictionary<int, UserIdentity>();

    public InMemoryUserIdentityRepository(IEnumerable<UserIdentity> users)
    {
        _users = users.ToDictionary(p => p.Id);
    }

    public Task AddAsync(UserIdentity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserIdentity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserIdentity> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public UserIdentity GetByUsername(string username)
    {
        return _users.Values.SingleOrDefault(u=>u.Username == username); 
    }

    public Task UpdateAsync(UserIdentity entity)
    {
        throw new NotImplementedException();
    }
}