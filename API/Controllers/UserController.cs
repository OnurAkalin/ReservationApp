using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly Guid _currentUserGuid;
    private readonly Guid _currentUserSelectedSiteId;

    public UserController(ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;

        try
        {
            if (httpContextAccessor.HttpContext == null) return;
            
            var siteId = httpContextAccessor.HttpContext.Request.Headers["SiteId"];
            
            if (!string.IsNullOrEmpty(siteId))
            {
                _currentUserSelectedSiteId = Guid.Parse(siteId);
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }
    
    
}