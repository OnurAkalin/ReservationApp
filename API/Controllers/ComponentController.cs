namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ComponentController : ControllerBase
{
    private readonly IComponentService _componentService;
    
    public ComponentController(IComponentService componentService)
    {
        _componentService = componentService;
    }
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<LoginComponentDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLogin()
        => Ok(await _componentService.GetLoginAsync());
    
    
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> SetLogin([FromBody] List<LoginComponentDto> requestDto)
        => Ok(await _componentService.SetLoginAsync(requestDto));
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFonts()
        => Ok(await _componentService.GetFontsAsync());
}
