namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(DataResult<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UploadImage([FromForm] ImageRequestDto request)
        => Ok(await _imageService.UploadToFileAsync(request.Image));

    [HttpGet]
    [ProducesResponseType(typeof(DataResult<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetImage(int id)
        => Ok(await _imageService.GetImagePathAsync(id));
    
    
    [HttpGet]
    [ProducesResponseType(typeof(DataResult<List<string>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListImages()
        => Ok(await _imageService.ListImagePathsAsync());
}