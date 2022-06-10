namespace Services;

public interface IAccountService
{
    Task<Result> RegisterAsync(RegisterRequestDto requestDto);
    Task<DataResult<TokenResponseDto>> LoginAsync(LoginRequestDto requestDto);
    Task<Result> ChangePasswordAsync(ChangePasswordRequestDto requestDto);
    Task<Result> ChangeEmailAsync(ChangeEmailRequestDto requestDto);
}