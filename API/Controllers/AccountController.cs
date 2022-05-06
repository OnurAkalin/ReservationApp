namespace API.Controllers;

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
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
    {
        var result = await _accountService.RegisterAsync(requestDto);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DataResult<TokenResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDataResult<TokenResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
    {
        var result = await _accountService.LoginAsync(requestDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }
}