namespace Services;

public interface IComponentService
{
    Task<Result> SetLoginAsync(List<LoginComponentDto> requestDto);
    Task<DataResult<List<LoginComponentDto>>> GetLoginAsync();
    Task<Result> SetRegisterAsync(List<RegisterComponentDto> requestDto);
    Task<DataResult<List<RegisterComponentDto>>> GetRegisterAsync();
    Task<Result> SetAuthLayoutAsync(List<AuthLayoutDto> requestDto);
    Task<DataResult<List<AuthLayoutDto>>> GetAuthLayoutAsync();
    Task<Result> SetCalendarConfigurationAsync(List<CalendarConfigurationDto> requestDto);
    Task<DataResult<List<CalendarConfigurationDto>>> GetCalendarConfigurationAsync();
    Task<Result> SetCalendarThemeAsync(List<CalendarThemeDto> requestDto);
    Task<DataResult<List<CalendarThemeDto>>> GetCalendarThemeAsync();
    Task<Result> SetCustomAsync(List<CustomDto> requestDto);
    Task<DataResult<List<CustomDto>>> GetCustomAsync();
}