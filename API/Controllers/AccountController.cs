using Core.Utilities.Results;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;

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
    public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
    {
        var result = await _accountService.LoginAsync(requestDto);
        return Ok(result);
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        var result = await _accountService.CreateRoleAsync(roleName);
        return Ok(result);
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateRole(Guid id, string roleName)
    {
        var result = await _accountService.UpdateRoleAsync(id, roleName);
        return Ok(result);
    }
    
    [HttpGet, AllowAnonymous]
    [ProducesResponseType(typeof(DataResult<List<RoleResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoles()
    {
        var result = await _accountService.GetRolesAsync();
        return Ok(result);
    }
    
    [HttpGet, Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWithToken()
    {
        return Ok("DATA");
    }
    
    [HttpGet, AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWithoutToken()
    {
        return Ok("DATA");
    }
}