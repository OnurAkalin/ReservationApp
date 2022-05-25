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
    public async Task<IActionResult> Login()
        => Ok(await _componentService.GetLoginAsync());


    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] List<LoginComponentDto> requestDto)
        => Ok(await _componentService.SetLoginAsync(requestDto));


    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<RegisterComponentDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register()
        => Ok(await _componentService.GetRegisterAsync());


    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] List<RegisterComponentDto> requestDto)
        => Ok(await _componentService.SetRegisterAsync(requestDto));
    
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<AuthLayoutDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AuthLayout()
        => Ok(await _componentService.GetAuthLayoutAsync());


    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> AuthLayout([FromBody] List<AuthLayoutDto> requestDto)
        => Ok(await _componentService.SetAuthLayoutAsync(requestDto));
    
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<CalendarConfigurationDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalendarConfiguration()
        => Ok(await _componentService.GetCalendarConfigurationAsync());


    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalendarConfiguration([FromBody] List<CalendarConfigurationDto> requestDto)
        => Ok(await _componentService.SetCalendarConfigurationAsync(requestDto));
    
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<CalendarThemeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalendarTheme()
        => Ok(await _componentService.GetCalendarThemeAsync());


    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalendarTheme([FromBody] List<CalendarThemeDto> requestDto)
        => Ok(await _componentService.SetCalendarThemeAsync(requestDto));
    
    
    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<CustomDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Custom()
        => Ok(await _componentService.GetCustomAsync());


    [AllowAnonymous]
    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Custom([FromBody] List<CustomDto> requestDto)
        => Ok(await _componentService.SetCustomAsync(requestDto));
}