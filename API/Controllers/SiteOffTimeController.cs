namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class SiteOffTimeController : ControllerBase
{
    private readonly ISiteService _siteService;

    public SiteOffTimeController(ISiteService siteService)
    {
        _siteService = siteService;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Insert(SiteOffTimeRequestDto requestDto)
        => Ok(await _siteService.InsertSiteOffTimeAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(SiteOffTimeRequestDto requestDto)
        => Ok(await _siteService.UpdateSiteOffTimeAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(DataResult<SiteOffTimeResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] int id)
        => Ok(await _siteService.GetSiteOffTimeAsync(id));


    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<SiteOffTimeResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List()
        => Ok(await _siteService.ListSiteOffTimeAsync());


    [HttpDelete]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromQuery] int id)
        => Ok(await _siteService.DeleteSiteOffTimeAsync(id));
}