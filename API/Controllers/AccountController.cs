namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
        => Ok(await _accountService.RegisterAsync(requestDto));


    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DataResult<TokenResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        => Ok(await _accountService.LoginAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto requestDto)
        => Ok(await _accountService.ChangePasswordAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailRequestDto requestDto)
        => Ok(await _accountService.ChangeEmailAsync(requestDto));
}