namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class CalendarController : ControllerBase
{
    private readonly ICalendarService _calendarService;

    public CalendarController(ICalendarService calendarService)
    {
        _calendarService = calendarService;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Insert(CalendarRequestDto requestDto)
        => Ok(await _calendarService.InsertAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(CalendarRequestDto requestDto)
        => Ok(await _calendarService.UpdateAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(DataResult<CalendarResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] int id)
        => Ok(await _calendarService.GetAsync(id));


    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<CalendarResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List()
        => Ok(await _calendarService.ListAsync());


    [HttpDelete]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromQuery] int id)
        => Ok(await _calendarService.DeleteAsync(id));
}