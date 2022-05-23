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
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Login));

        if (currentLoginComponent is not null)
        {
            _dbContext.Components.Remove(currentLoginComponent);
        }

        var loginComponent = new Component
        {
            Type = ComponentType.Login,
            Value = JsonConvert.SerializeObject(requestDto)
        };

        await _dbContext.Components.AddAsync(loginComponent);
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
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Register));

        if (currentRegisterComponent is not null)
        {
            _dbContext.Components.Remove(currentRegisterComponent);
        }

        var registerComponent = new Component
        {
            Type = ComponentType.Register,
            Value = JsonConvert.SerializeObject(requestDto)
        };

        await _dbContext.Components.AddAsync(registerComponent);
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

    public async Task<Result> SetAuthLayoutAsync(List<AuthLayoutDto> requestDto)
    {
        var currentAuthLayoutComponent = await _dbContext.Components
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.AuthLayout));

        if (currentAuthLayoutComponent is not null)
        {
            _dbContext.Components.Remove(currentAuthLayoutComponent);
        }

        var authLayoutComponent = new Component
        {
            Type = ComponentType.AuthLayout,
            Value = JsonConvert.SerializeObject(requestDto)
        };

        await _dbContext.Components.AddAsync(authLayoutComponent);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<AuthLayoutDto>>> GetAuthLayoutAsync()
    {
        var authLayoutComponent = await _dbContext.Components
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.AuthLayout));

        if (authLayoutComponent is null)
        {
            return new ErrorDataResult<List<AuthLayoutDto>>(UiMessages.NotFoundData);
        }

        var jsonData = JsonConvert.DeserializeObject<List<AuthLayoutDto>>(authLayoutComponent.Value);

        return new SuccessDataResult<List<AuthLayoutDto>>(jsonData!, UiMessages.Success);
    }

    public async Task<Result> SetCalendarConfigurationAsync(List<CalendarConfigurationDto> requestDto)
    {
        var currentCalendarConfigurationComponent = await _dbContext.Components
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.CalendarConfiguration));

        if (currentCalendarConfigurationComponent is not null)
        {
            _dbContext.Components.Remove(currentCalendarConfigurationComponent);
        }

        var calendarConfigurationComponent = new Component
        {
            Type = ComponentType.CalendarConfiguration,
            Value = JsonConvert.SerializeObject(requestDto)
        };

        await _dbContext.Components.AddAsync(calendarConfigurationComponent);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<CalendarConfigurationDto>>> GetCalendarConfigurationAsync()
    {
        var calendarConfigurationComponent = await _dbContext.Components
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.CalendarConfiguration));

        if (calendarConfigurationComponent is null)
        {
            return new ErrorDataResult<List<CalendarConfigurationDto>>(UiMessages.NotFoundData);
        }

        var jsonData = JsonConvert.DeserializeObject<List<CalendarConfigurationDto>>(calendarConfigurationComponent.Value);

        return new SuccessDataResult<List<CalendarConfigurationDto>>(jsonData!, UiMessages.Success);
    }

    public async Task<Result> SetCalendarThemeAsync(List<CalendarThemeDto> requestDto)
    {
        var currentCalendarThemeComponent = await _dbContext.Components
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.CalendarTheme));

        if (currentCalendarThemeComponent is not null)
        {
            _dbContext.Components.Remove(currentCalendarThemeComponent);
        }

        var calendarThemeComponent = new Component
        {
            Type = ComponentType.CalendarTheme,
            Value = JsonConvert.SerializeObject(requestDto)
        };

        await _dbContext.Components.AddAsync(calendarThemeComponent);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<CalendarThemeDto>>> GetCalendarThemeAsync()
    {
        var calendarThemeComponent = await _dbContext.Components
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.CalendarTheme));

        if (calendarThemeComponent is null)
        {
            return new ErrorDataResult<List<CalendarThemeDto>>(UiMessages.NotFoundData);
        }

        var jsonData = JsonConvert.DeserializeObject<List<CalendarThemeDto>>(calendarThemeComponent.Value);

        return new SuccessDataResult<List<CalendarThemeDto>>(jsonData!, UiMessages.Success);
    }

    public async Task<Result> SetCustomAsync(List<CustomDto> requestDto)
    {
        var currentCustomComponent = await _dbContext.Components
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Custom));

        if (currentCustomComponent is not null)
        {
            _dbContext.Components.Remove(currentCustomComponent);
        }

        var customComponent = new Component
        {
            Type = ComponentType.Custom,
            Value = JsonConvert.SerializeObject(requestDto)
        };

        await _dbContext.Components.AddAsync(customComponent);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<List<CustomDto>>> GetCustomAsync()
    {
        var customComponent = await _dbContext.Components
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Type.Equals(ComponentType.Custom));

        if (customComponent is null)
        {
            return new ErrorDataResult<List<CustomDto>>(UiMessages.NotFoundData);
        }

        var jsonData = JsonConvert.DeserializeObject<List<CustomDto>>(customComponent.Value);

        return new SuccessDataResult<List<CustomDto>>(jsonData!, UiMessages.Success);
    }
}