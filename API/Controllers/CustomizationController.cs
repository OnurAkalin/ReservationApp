using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CustomizationController : ControllerBase
{
    private readonly ILogger<CustomizationController> _logger;

    public CustomizationController (ILogger<CustomizationController> logger)
    {
        _logger = logger;
    }
}