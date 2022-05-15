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
        => Ok(await _roleService.CreateRoleAsync(roleName));


    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateRole(int id, string roleName)
        => Ok(await _roleService.UpdateRoleAsync(id, roleName));


    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<RoleResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoles()
        => Ok(await _roleService.GetRolesAsync());
}