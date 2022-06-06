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
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Insert(string roleName)
        => Ok(await _roleService.InsertAsync(roleName));


    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(int id, string roleName)
        => Ok(await _roleService.UpdateAsync(id, roleName));


    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<RoleResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(int id)
        => Ok(await _roleService.GetAsync(id));


    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<RoleResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List()
        => Ok(await _roleService.ListAsync());


    [HttpDelete]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _roleService.DeleteAsync(id));
}