using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ImageController
{
    private readonly ILogger<ImageController> _logger;

    public ImageController (ILogger<ImageController> logger)
    {
        _logger = logger;
    }
}