﻿using Newtonsoft.Json;
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

    public async Task<Result> SetRegisterAsync(List<RegisterComponentDto> requestDto)
    {
        var currentRegisterComponent = await _dbContext.Components
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Register));

        if (currentRegisterComponent is not null)
        {
            _dbContext.Remove(currentRegisterComponent);
        }

        var loginComponent = new Component
        {
            Type = ComponentType.Register,
            Value = JsonConvert.SerializeObject(requestDto)
        };

        await _dbContext.AddAsync(loginComponent);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<RegisterComponentDto>>> GetRegisterAsync()
    {
        var registerComponent = await _dbContext.Components
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Register));

        if (registerComponent is null)
        {
            return new ErrorDataResult<List<RegisterComponentDto>>(UiMessages.NotFoundData);
        }

        var jsonData = JsonConvert.DeserializeObject<List<RegisterComponentDto>>(registerComponent.Value);

        return new SuccessDataResult<List<RegisterComponentDto>>(jsonData!, UiMessages.Success);
    }
}