global using AutoMapper;
global using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Services;

public class BasicService
{
    protected readonly ILogger _logger;
    protected readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly IMapper _mapper;
    
    protected readonly Guid _currentUserGuid;
    protected readonly Guid _currentUserSelectedSiteId;

    protected BasicService
    (
        ILogger logger,
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper
    )
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;

        _currentUserSelectedSiteId = Guid.Parse(httpContextAccessor.HttpContext.Request.Headers["SiteId"]);
        _currentUserGuid = Guid.Parse((ReadOnlySpan<char>) _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value);
    }
}