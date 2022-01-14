using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CalendarController : ControllerBase
{
    private readonly ILogger<CalendarController> _logger;

    public CalendarController (ILogger<CalendarController> logger)
    {
        _logger = logger;
    }
}