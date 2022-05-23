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
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> SetLogin([FromBody] List<LoginComponentDto> requestDto)
        => Ok(await _componentService.SetLoginAsync(requestDto));


    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<RegisterComponentDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRegister()
        => Ok(await _componentService.GetRegisterAsync());


    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> SetRegister([FromBody] List<RegisterComponentDto> requestDto)
        => Ok(await _componentService.SetRegisterAsync(requestDto));
    
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<AuthLayoutDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthLayout()
        => Ok(await _componentService.GetAuthLayoutAsync());


    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> SetAuthLayout([FromBody] List<AuthLayoutDto> requestDto)
        => Ok(await _componentService.SetAuthLayoutAsync(requestDto));
    
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<CalendarConfigurationDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCalendarConfiguration()
        => Ok(await _componentService.GetCalendarConfigurationAsync());


    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> SetCalendarConfiguration([FromBody] List<CalendarConfigurationDto> requestDto)
        => Ok(await _componentService.SetCalendarConfigurationAsync(requestDto));
    
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<CalendarThemeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCalendarTheme()
        => Ok(await _componentService.GetCalendarThemeAsync());


    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> SetCalendarTheme([FromBody] List<CalendarThemeDto> requestDto)
        => Ok(await _componentService.SetCalendarThemeAsync(requestDto));
    
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<CustomDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustom()
        => Ok(await _componentService.GetCustomAsync());


    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> SetCustom([FromBody] List<CustomDto> requestDto)
        => Ok(await _componentService.SetCustomAsync(requestDto));
}