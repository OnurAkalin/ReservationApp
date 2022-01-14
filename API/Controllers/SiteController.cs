using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SiteController : ControllerBase
{
    private readonly ILogger<SiteController> _logger;

    public SiteController(ILogger<SiteController> logger)
    {
        _logger = logger;
    }
}