using Core.Jwt;

namespace Services;

public interface ITokenService
{
    Task<AccessToken> GenerateTokenAsync(User user);
}