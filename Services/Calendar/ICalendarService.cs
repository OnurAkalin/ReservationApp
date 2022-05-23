namespace Services;

public interface ICalendarService
{
    Task<Result> InsertAsync(CalendarRequestDto requestDto);
    Task<Result> UpdateAsync(CalendarRequestDto requestDto);
    Task<DataResult<List<CalendarResponseDto>>> ListAsync();
    Task<DataResult<CalendarResponseDto>> GetAsync(int id);
    Task<Result> DeleteAsync(int id);
}