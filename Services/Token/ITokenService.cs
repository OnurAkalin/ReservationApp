namespace Services;

public interface ITokenService
{
    Task<TokenResponseDto> GenerateTokenAsync(User user);
}