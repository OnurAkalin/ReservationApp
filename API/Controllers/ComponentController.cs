using Domain.Constants;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ComponentController : ControllerBase
{
    public ComponentController()
    {
    }


    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<CalendarComponent>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCalendar()
        => Ok(new SuccessDataResult<List<CalendarComponent>>(new List<CalendarComponent>()
        {
            new CalendarComponent(){
                Id = 1,
                HourDuration = "20",
                Locale = "tr"
            },
            new CalendarComponent(){
                Id = 2,
                HourDuration = "30",
                Locale = "tr"
            },
        }, UiMessages.Success));
    
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(DataResult<CalendarComponent>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SetCalendar(List<CalendarComponent> requestDto)
        => Ok(new SuccessDataResult<CalendarComponent>(new CalendarComponent
        {
            Id = 1,
            HourDuration = "20",
            Locale = "tr"
        }, UiMessages.Success));
}

public class CalendarComponent
{
    public int Id { get; set; }
    public string HourDuration { get; set; }
    public string Locale { get; set; }
}