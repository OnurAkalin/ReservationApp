using Newtonsoft.Json;
using StackExchange.Redis;

namespace Services;

public class ComponentService : BasicService, IComponentService
{
    private readonly IDatabase _redis;

    public ComponentService
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


    public async Task<Result> SetLoginAsync(List<LoginComponentDto> requestDto)
    {
        var currentLoginComponent = await _dbContext.Components
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Login));

        if (currentLoginComponent is not null)
        {
            _dbContext.Remove(currentLoginComponent);
        }

        var loginComponent = new Component
        {
            Type = ComponentType.Login,
            Value = JsonConvert.SerializeObject(requestDto)
        };

        await _dbContext.AddAsync(loginComponent);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<LoginComponentDto>>> GetLoginAsync()
    {
        var loginComponent = await _dbContext.Components
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Login));

        if (loginComponent is null)
        {
            return new ErrorDataResult<List<LoginComponentDto>>(UiMessages.NotFoundData);
        }

        var jsonData = JsonConvert.DeserializeObject<List<LoginComponentDto>>(loginComponent.Value);

        return new SuccessDataResult<List<LoginComponentDto>>(jsonData!, UiMessages.Success);
    }
}