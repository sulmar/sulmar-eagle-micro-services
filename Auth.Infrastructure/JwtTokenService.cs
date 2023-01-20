using Auth.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.Infrastructure;


public class JwtTokenService : ITokenService
{

    // dotnet add package System.IdentityModel.Tokens.Jwt
    public string CreateToken(UserIdentity user)
    {
        string secretKey = "your-256-bit-secret";
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Username));
        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.Phone));

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "http://eagle.pl",
            Audience = "http://sulmar.pl",
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = credentials,
            Subject = identity
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);  

        return tokenHandler.WriteToken(token);


    }

    public void CancelToken(string token)
    {
        throw new NotImplementedException();
    }

    
}
