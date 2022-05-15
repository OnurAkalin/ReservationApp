namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser([FromQuery] int id)
        => Ok(await _userService.GetUserAsync(id));
    
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto requestDto)
        => Ok(await _userService.UpdateUserAsync(requestDto));
    
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteUser([FromQuery] int id)
        => Ok(await _userService.DeleteUserAsync(id));
    
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddToRole([FromQuery] int userId, [FromQuery] int roleId)
        => Ok(await _userService.AddToRoleAsync(userId, roleId));
    
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteFromRole([FromQuery] int userId, [FromQuery] int roleId)
        => Ok(await _userService.DeleteFromRoleAsync(userId, roleId));
}