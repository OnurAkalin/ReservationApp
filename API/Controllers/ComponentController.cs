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
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCalendar()
        => Ok(new SuccessResult("Yapılacak"));
    
    
    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> SetCalendar()
        => Ok(new SuccessResult("Yapılacak"));
}
