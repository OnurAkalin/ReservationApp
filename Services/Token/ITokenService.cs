using Core.Jwt;

namespace Services;

public interface ITokenService
{
    Task<AccessToken> GenerateToken(User user);
}