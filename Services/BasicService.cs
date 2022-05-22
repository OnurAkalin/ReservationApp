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
        if (!string.IsNullOrWhiteSpace(siteId))
        {
            _currentSiteId = int.Parse(siteId);
        }

        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrWhiteSpace(userId))
        {
            _currentUserId = int.Parse(userId);
        }
    }
}