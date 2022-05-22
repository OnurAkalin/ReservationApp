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
        IConnectionMultiplexer redis
    )
        : base(logger, mapper, dbContext)
    {
        _redis = redis.GetDatabase((int) RedisDatabases.Component);
    }
}