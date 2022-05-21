namespace Services;

public interface ITokenService
{
    Task<TokenResponseDto> GenerateAsync(User user);
}