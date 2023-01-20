namespace Auth.Domain;

public interface ITokenService
{
    string CreateToken(UserIdentity user);
    void CancelToken(string token);
}


