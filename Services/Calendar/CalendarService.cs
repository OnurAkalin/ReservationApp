namespace Services;

public class CalendarService : BasicService, ICalendarService
{
    public CalendarService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor
    )
        : base(logger, mapper, dbContext, httpContextAccessor)
    {
    }

    public async Task<Result> InsertAsync(CalendarRequestDto requestDto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UpdateAsync(CalendarRequestDto requestDto)
    {
        throw new NotImplementedException();
    }

    public async Task<DataResult<List<CalendarResponseDto>>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<DataResult<CalendarResponseDto>> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}