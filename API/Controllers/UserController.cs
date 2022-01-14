using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<UserController> _logger;
    private readonly Guid _currentUserGuid;
    private readonly Guid _currentUserSelectedSiteId;

    public UserController(ILogger<UserController> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;

        try
        {
            if (_httpContextAccessor.HttpContext == null) return;
            var siteId = _httpContextAccessor.HttpContext.Request.Headers["SiteId"];
            if (!string.IsNullOrEmpty(siteId))
            {
                _currentUserSelectedSiteId = Guid.Parse(siteId);
            }
        }
        catch (Exception ex)
        {
            //_logger.Error(ex.ToString(), "{message}", "Header SiteId is null");
        }
    }
    
    
}