namespace API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]/[action]")]
public class SiteController : ControllerBase
{
    private readonly ISiteService _siteService;

    public SiteController(ISiteService siteService)
    {
        _siteService = siteService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Insert(SiteRequestDto requestDto)
        => Ok(await _siteService.InsertSiteAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(SiteRequestDto requestDto)
        => Ok(await _siteService.UpdateSiteAsync(requestDto));


    [HttpPost]
    [ProducesResponseType(typeof(DataResult<SiteResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] int id)
        => Ok(await _siteService.GetSiteAsync(id));


    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(DataResult<List<SiteResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> List()
        => Ok(await _siteService.ListSiteAsync());


    [HttpDelete]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> Delete([FromQuery] int id)
        => Ok(await _siteService.DeleteSiteAsync(id));


    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadImage([FromForm] ImageRequestDto requestDto)
        => Ok(await _siteService.UploadSiteImageAsync(requestDto));
}