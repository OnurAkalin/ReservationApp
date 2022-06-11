namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class SiteServiceController : ControllerBase
{
    private readonly ISiteService _siteService;

    public SiteServiceController(ISiteService siteService)
    {
        _siteService = siteService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Insert(SiteServiceRequestDto requestDto)
        => Ok(await _siteService.InsertSiteServiceAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(SiteServiceRequestDto requestDto)
        => Ok(await _siteService.UpdateSiteServiceAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(DataResult<SiteServiceResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] int id)
        => Ok(await _siteService.GetSiteServiceAsync(id));


    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<SiteServiceResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List()
        => Ok(await _siteService.ListSiteServiceAsync());


    [HttpDelete]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromQuery] int id)
        => Ok(await _siteService.DeleteSiteServiceAsync(id));
    
}