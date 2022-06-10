namespace Services;

public class BasicService
{
    protected readonly Logger _logger;
    protected readonly IMapper _mapper;
    protected readonly ApplicationDbContext _dbContext;
    protected readonly int _currentSiteId;
    protected readonly int _currentUserId;

    protected BasicService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _logger = logger;
        _mapper = mapper;
        _dbContext = dbContext;

        var siteId = httpContextAccessor.HttpContext?.Request.Headers["SiteId"];
        _currentSiteId = !string.IsNullOrWhiteSpace(siteId) ? int.Parse(siteId) : default;

        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _currentUserId = !string.IsNullOrWhiteSpace(userId) ? int.Parse(userId) : default;
    }
}