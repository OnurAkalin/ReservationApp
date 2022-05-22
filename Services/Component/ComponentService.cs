using StackExchange.Redis;

namespace Services;

public class ComponentService : BasicService, IComponentService
{
    private readonly IDatabase _redis;

    protected ComponentService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        IConnectionMultiplexer redis
    )
        : base(logger, mapper, dbContext, httpContextAccessor)
    {
        _redis = redis.GetDatabase((int) RedisDatabases.Component);
    }
}