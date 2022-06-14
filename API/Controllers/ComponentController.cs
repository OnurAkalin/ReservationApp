namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class ComponentController : ControllerBase
{
    private readonly IComponentService _componentService;

    public ComponentController(IComponentService componentService)
    {
        _componentService = componentService;
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DataResult<List<LoginComponentDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login()
        => Ok(await _componentService.GetLoginAsync());


    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] List<LoginComponentDto> requestDto)
        => Ok(await _componentService.SetLoginAsync(requestDto));


    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DataResult<List<RegisterComponentDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register()
        => Ok(await _componentService.GetRegisterAsync());


    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] List<RegisterComponentDto> requestDto)
        => Ok(await _componentService.SetRegisterAsync(requestDto));


    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DataResult<List<AuthLayoutDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> AuthLayout()
        => Ok(await _componentService.GetAuthLayoutAsync());


    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> AuthLayout([FromBody] List<AuthLayoutDto> requestDto)
        => Ok(await _componentService.SetAuthLayoutAsync(requestDto));
    
    
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DataResult<List<CalendarLayoutDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalendarLayout()
        => Ok(await _componentService.GetCalendarLayoutAsync());


    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalendarLayout([FromBody] List<CalendarLayoutDto> requestDto)
        => Ok(await _componentService.SetCalendarLayoutAsync(requestDto));


    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DataResult<List<CalendarConfigurationDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalendarConfiguration()
        => Ok(await _componentService.GetCalendarConfigurationAsync());


    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> CalendarConfiguration([FromBody] List<CalendarConfigurationDto> requestDto)
        => Ok(await _componentService.SetCalendarConfigurationAsync(requestDto));


    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DataResult<List<CustomDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Custom()
        => Ok(await _componentService.GetCustomAsync());


    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Custom([FromBody] List<CustomDto> requestDto)
        => Ok(await _componentService.SetCustomAsync(requestDto));


    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DataResult<List<WebPageDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> WebPage()
        => Ok(await _componentService.GetWebPageAsync());


    [HttpPut]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> WebPage([FromBody] List<WebPageDto> requestDto)
        => Ok(await _componentService.SetWebPageAsync(requestDto));
}