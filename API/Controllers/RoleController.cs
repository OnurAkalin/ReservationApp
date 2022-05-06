namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        var result = await _roleService.CreateRoleAsync(roleName);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SuccessResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRole(Guid id, string roleName)
    {
        var result = await _roleService.UpdateRoleAsync(id, roleName);

        if (!result.Success)
        {
            return BadRequest(new ErrorResult(result.Message));
        }
        
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<RoleResponseDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResult), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetRoles()
    {
        var result = await _roleService.GetRolesAsync();

        if (!result.Success)
        {
            return BadRequest(new ErrorResult(result.Message));
        }

        return Ok(result);
    }
}