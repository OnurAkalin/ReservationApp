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
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Login)
                                      && x.SiteId.Equals(_currentSiteId));

        if (currentLoginComponent is not null)
        {
            _dbContext.Components.Remove(currentLoginComponent);
        }

        var loginComponent = new Component
        {
            Type = ComponentType.Login,
            Value = JsonConvert.SerializeObject(requestDto),
            SiteId = _currentSiteId
        };

        await _dbContext.Components.AddAsync(loginComponent);
        await _dbContext.SaveChangesAsync();
        
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.Login.ToString());
        await _redis.KeyDeleteAsync(redisKey);

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<LoginComponentDto>>> GetLoginAsync()
    {
        List<LoginComponentDto> responseData;
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.Login.ToString());
        var redisValue = await _redis.StringGetAsync(redisKey);
        
        if (redisValue.IsNull)
        {
            var loginComponent = await _dbContext.Components
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Login)
                                          && x.SiteId.Equals(_currentSiteId));
            
            if (loginComponent is null)
            {
                return new ErrorDataResult<List<LoginComponentDto>>(UiMessages.NotFoundData);
            }
            
            responseData = JsonConvert.DeserializeObject<List<LoginComponentDto>>(loginComponent.Value);

            await _redis.StringSetAsync(redisKey, loginComponent.Value);
        }
        else
        {
             responseData = JsonConvert.DeserializeObject<List<LoginComponentDto>>(redisValue);
        }
        
        return new SuccessDataResult<List<LoginComponentDto>>(responseData, UiMessages.Success);
    }

    public async Task<Result> SetRegisterAsync(List<RegisterComponentDto> requestDto)
    {
        var currentRegisterComponent = await _dbContext.Components
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Register)
                                      && x.SiteId.Equals(_currentSiteId));

        if (currentRegisterComponent is not null)
        {
            _dbContext.Components.Remove(currentRegisterComponent);
        }

        var registerComponent = new Component
        {
            Type = ComponentType.Register,
            Value = JsonConvert.SerializeObject(requestDto),
            SiteId = _currentSiteId
        };

        await _dbContext.Components.AddAsync(registerComponent);
        await _dbContext.SaveChangesAsync();
        
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.Register.ToString());
        await _redis.KeyDeleteAsync(redisKey);

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<RegisterComponentDto>>> GetRegisterAsync()
    {
        List<RegisterComponentDto> responseData;
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.Register.ToString());
        var redisValue = await _redis.StringGetAsync(redisKey);
        
        if (redisValue.IsNull)
        {
            var registerComponent = await _dbContext.Components
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Register)
                                          && x.SiteId.Equals(_currentSiteId));
            
            if (registerComponent is null)
            {
                return new ErrorDataResult<List<RegisterComponentDto>>(UiMessages.NotFoundData);
            }
            
            responseData = JsonConvert.DeserializeObject<List<RegisterComponentDto>>(registerComponent.Value);

            await _redis.StringSetAsync(redisKey, registerComponent.Value);
        }
        else
        {
            responseData = JsonConvert.DeserializeObject<List<RegisterComponentDto>>(redisValue);
        }
        
        return new SuccessDataResult<List<RegisterComponentDto>>(responseData, UiMessages.Success);
    }

    public async Task<Result> SetAuthLayoutAsync(List<AuthLayoutDto> requestDto)
    {
        var currentAuthLayoutComponent = await _dbContext.Components
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.AuthLayout)
                                      && x.SiteId.Equals(_currentSiteId));

        if (currentAuthLayoutComponent is not null)
        {
            _dbContext.Components.Remove(currentAuthLayoutComponent);
        }

        var authLayoutComponent = new Component
        {
            Type = ComponentType.AuthLayout,
            Value = JsonConvert.SerializeObject(requestDto),
            SiteId = _currentSiteId
        };

        await _dbContext.Components.AddAsync(authLayoutComponent);
        await _dbContext.SaveChangesAsync();
        
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.AuthLayout.ToString());
        await _redis.KeyDeleteAsync(redisKey);

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<AuthLayoutDto>>> GetAuthLayoutAsync()
    {
        List<AuthLayoutDto> responseData;
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.AuthLayout.ToString());
        var redisValue = await _redis.StringGetAsync(redisKey);
        
        if (redisValue.IsNull)
        {
            var authLayoutComponent = await _dbContext.Components
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.AuthLayout)
                                          && x.SiteId.Equals(_currentSiteId));
            
            if (authLayoutComponent is null)
            {
                return new ErrorDataResult<List<AuthLayoutDto>>(UiMessages.NotFoundData);
            }
            
            responseData = JsonConvert.DeserializeObject<List<AuthLayoutDto>>(authLayoutComponent.Value);

            await _redis.StringSetAsync(redisKey, authLayoutComponent.Value);
        }
        else
        {
            responseData = JsonConvert.DeserializeObject<List<AuthLayoutDto>>(redisValue);
        }
        
        return new SuccessDataResult<List<AuthLayoutDto>>(responseData, UiMessages.Success);
    }
    
    public async Task<Result> SetCalendarLayoutAsync(List<CalendarLayoutDto> requestDto)
    {
        var currentCalendarLayoutComponent = await _dbContext.Components
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.CalendarLayout)
                                      && x.SiteId.Equals(_currentSiteId));

        if (currentCalendarLayoutComponent is not null)
        {
            _dbContext.Components.Remove(currentCalendarLayoutComponent);
        }

        var calendarLayoutComponent = new Component
        {
            Type = ComponentType.CalendarLayout,
            Value = JsonConvert.SerializeObject(requestDto),
            SiteId = _currentSiteId
        };

        await _dbContext.Components.AddAsync(calendarLayoutComponent);
        await _dbContext.SaveChangesAsync();
        
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.CalendarLayout.ToString());
        await _redis.KeyDeleteAsync(redisKey);

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<CalendarLayoutDto>>> GetCalendarLayoutAsync()
    {
        List<CalendarLayoutDto> responseData;
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.CalendarLayout.ToString());
        var redisValue = await _redis.StringGetAsync(redisKey);
        
        if (redisValue.IsNull)
        {
            var calendarLayoutComponent = await _dbContext.Components
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.CalendarLayout)
                                          && x.SiteId.Equals(_currentSiteId));
            
            if (calendarLayoutComponent is null)
            {
                return new ErrorDataResult<List<CalendarLayoutDto>>(UiMessages.NotFoundData);
            }
            
            responseData = JsonConvert.DeserializeObject<List<CalendarLayoutDto>>(calendarLayoutComponent.Value);

            await _redis.StringSetAsync(redisKey, calendarLayoutComponent.Value);
        }
        else
        {
            responseData = JsonConvert.DeserializeObject<List<CalendarLayoutDto>>(redisValue);
        }
        
        return new SuccessDataResult<List<CalendarLayoutDto>>(responseData, UiMessages.Success);
    }

    public async Task<Result> SetCalendarConfigurationAsync(List<CalendarConfigurationDto> requestDto)
    {
        var currentCalendarConfigurationComponent = await _dbContext.Components
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.CalendarConfiguration)
                                      && x.SiteId.Equals(_currentSiteId));

        if (currentCalendarConfigurationComponent is not null)
        {
            _dbContext.Components.Remove(currentCalendarConfigurationComponent);
        }

        var calendarConfigurationComponent = new Component
        {
            Type = ComponentType.CalendarConfiguration,
            Value = JsonConvert.SerializeObject(requestDto),
            SiteId = _currentSiteId
        };

        await _dbContext.Components.AddAsync(calendarConfigurationComponent);
        await _dbContext.SaveChangesAsync();
        
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.CalendarConfiguration.ToString());
        await _redis.KeyDeleteAsync(redisKey);

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<CalendarConfigurationDto>>> GetCalendarConfigurationAsync()
    {
        List<CalendarConfigurationDto> responseData;
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.CalendarConfiguration.ToString());
        var redisValue = await _redis.StringGetAsync(redisKey);
        
        if (redisValue.IsNull)
        {
            var calendarConfigurationComponent = await _dbContext.Components
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.CalendarConfiguration)
                                          && x.SiteId.Equals(_currentSiteId));
            
            if (calendarConfigurationComponent is null)
            {
                return new ErrorDataResult<List<CalendarConfigurationDto>>(UiMessages.NotFoundData);
            }
            
            responseData = JsonConvert.DeserializeObject<List<CalendarConfigurationDto>>(calendarConfigurationComponent.Value);

            await _redis.StringSetAsync(redisKey, calendarConfigurationComponent.Value);
        }
        else
        {
            responseData = JsonConvert.DeserializeObject<List<CalendarConfigurationDto>>(redisValue);
        }
        
        return new SuccessDataResult<List<CalendarConfigurationDto>>(responseData, UiMessages.Success);
    }

    public async Task<Result> SetCustomAsync(List<CustomDto> requestDto)
    {
        var currentCustomComponent = await _dbContext.Components
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Custom)
                                      && x.SiteId.Equals(_currentSiteId));

        if (currentCustomComponent is not null)
        {
            _dbContext.Components.Remove(currentCustomComponent);
        }

        var customComponent = new Component
        {
            Type = ComponentType.Custom,
            Value = JsonConvert.SerializeObject(requestDto),
            SiteId = _currentSiteId
        };

        await _dbContext.Components.AddAsync(customComponent);
        await _dbContext.SaveChangesAsync();
        
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.Custom.ToString());
        await _redis.KeyDeleteAsync(redisKey);

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<CustomDto>>> GetCustomAsync()
    {
        List<CustomDto> responseData;
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.Custom.ToString());
        var redisValue = await _redis.StringGetAsync(redisKey);
        
        if (redisValue.IsNull)
        {
            var customComponent = await _dbContext.Components
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Custom)
                                          && x.SiteId.Equals(_currentSiteId));
            
            if (customComponent is null)
            {
                return new ErrorDataResult<List<CustomDto>>(UiMessages.NotFoundData);
            }
            
            responseData = JsonConvert.DeserializeObject<List<CustomDto>>(customComponent.Value);

            await _redis.StringSetAsync(redisKey, customComponent.Value);
        }
        else
        {
            responseData = JsonConvert.DeserializeObject<List<CustomDto>>(redisValue);
        }
        
        return new SuccessDataResult<List<CustomDto>>(responseData, UiMessages.Success);
    }

    public async Task<Result> SetWebPageAsync(List<WebPageDto> requestDto)
    {
        var currentWebPageComponent = await _dbContext.Components
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.WebPage)
                                      && x.SiteId.Equals(_currentSiteId));

        if (currentWebPageComponent is not null)
        {
            _dbContext.Components.Remove(currentWebPageComponent);
        }

        var webPageComponent = new Component
        {
            Type = ComponentType.WebPage,
            Value = JsonConvert.SerializeObject(requestDto),
            SiteId = _currentSiteId
        };

        await _dbContext.Components.AddAsync(webPageComponent);
        await _dbContext.SaveChangesAsync();
        
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.WebPage.ToString());
        await _redis.KeyDeleteAsync(redisKey);

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<WebPageDto>>> GetWebPageAsync()
    {
        List<WebPageDto> responseData;
        var redisKey = string.Format(CacheKeys.Component, _currentSiteId, ComponentType.WebPage.ToString());
        var redisValue = await _redis.StringGetAsync(redisKey);
        
        if (redisValue.IsNull)
        {
            var webPageComponent = await _dbContext.Components
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.WebPage)
                                          && x.SiteId.Equals(_currentSiteId));
            
            if (webPageComponent is null)
            {
                return new ErrorDataResult<List<WebPageDto>>(UiMessages.NotFoundData);
            }
            
            responseData = JsonConvert.DeserializeObject<List<WebPageDto>>(webPageComponent.Value);

            await _redis.StringSetAsync(redisKey, webPageComponent.Value);
        }
        else
        {
            responseData = JsonConvert.DeserializeObject<List<WebPageDto>>(redisValue);
        }
        
        return new SuccessDataResult<List<WebPageDto>>(responseData, UiMessages.Success);
    }
}