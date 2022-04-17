/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AccountController> _logger;
    private readonly Guid _currentUserGuid;
    private readonly Guid _currentUserSelectedSiteId;

    public AccountController(ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;

        try
        {
            if (_httpContextAccessor.HttpContext == null) return;
            
            var siteId = _httpContextAccessor.HttpContext?.Request.Headers["SiteId"];
            if (!string.IsNullOrEmpty(siteId))
            {
                _currentUserSelectedSiteId = Guid.Parse(siteId);
            }
        }
        catch (Exception ex)
        {
            //_logger.Error(ex.ToString(), "{message}", "Header SiteId is null");
        }
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(SampleSuccessResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SampleErrorResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] SampleRequestDto requestDto)
    {
        return null;

        //var result = await _accountService.Login(requestDto);
//
        //if (!result)
        //{
        //    return BadRequest(new SampleErrorResponseDto
        //    {
        //        Message = result.Message
        //    });
        //}
//
        //return Ok(new SampleSuccessResponseDto
        //{
        //    Message = result.Message
        //});
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(SampleSuccessResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SampleErrorResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgotPassword([FromBody] SampleRequestDto requestDto)
    {
        await Task.CompletedTask;
        return null;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(SampleSuccessResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SampleErrorResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword([FromBody] SampleRequestDto requestDto)
    {
        await Task.CompletedTask;
        return null;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUserWithOtp([FromBody] SampleRequestDto requestDto)
    {
        await Task.CompletedTask;
        return null;
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(SampleSuccessResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SampleErrorResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUser([FromBody] SampleRequestDto requestDto)
    {
        await Task.CompletedTask;
        return null;
    }
}*/