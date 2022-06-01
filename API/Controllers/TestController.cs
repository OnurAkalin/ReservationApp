namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TestController : ControllerBase
{
    private readonly IImageService _imageService;

    public TestController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(DataResult<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadImage([FromForm] FileDto request)
        => Ok(await _imageService.UploadToFileAsync(request.File));
    
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetImage(int id)
        => Ok(await _imageService.GetImagePathAsync(id));
}

public class FileDto
{
    public IFormFile File { get; set; }
}