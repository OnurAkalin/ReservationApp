namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class SiteServiceDayController : ControllerBase
{
    private readonly ISiteService _siteService;

    public SiteServiceDayController(ISiteService siteService)
    {
        _siteService = siteService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Insert(SiteServiceDayRequestDto requestDto)
        => Ok(await _siteService.InsertSiteServiceDayAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(SiteServiceDayRequestDto requestDto)
        => Ok(await _siteService.UpdateSiteServiceDayAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(DataResult<SiteServiceDayResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] int id)
        => Ok(await _siteService.GetSiteServiceDayAsync(id));


    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<SiteServiceDayResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List()
        => Ok(await _siteService.ListSiteServiceDayAsync());


    [HttpDelete]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromQuery] int id)
        => Ok(await _siteService.DeleteSiteServiceDayAsync(id));
}