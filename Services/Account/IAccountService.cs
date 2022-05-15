namespace Services;

public interface IAccountService
{
    Task<Result> RegisterAsync(RegisterRequestDto requestDto);
    Task<DataResult<TokenResponseDto>> LoginAsync(LoginRequestDto requestDto);
    Task<Result> ChangePassword(ChangePasswordRequestDto requestDto);
    Task<Result> ChangeEmail(ChangeEmailRequestDto requestDto);

}