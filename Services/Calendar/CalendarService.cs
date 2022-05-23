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
        var calendar = _mapper.Map<Calendar>(requestDto);
        calendar.CreateDate = DateTime.Now;
        calendar.CreateUser = _currentUserId;
        calendar.SiteId = _currentSiteId;
        calendar.UserId = requestDto.UserId ?? _currentUserId;

        await _dbContext.Calendars.AddAsync(calendar);
        var result = await _dbContext.SaveChangesAsync();

        if (result > 0)
        {
            return new SuccessResult(UiMessages.Success);
        }

        return new ErrorResult(UiMessages.Error);
    }

    public async Task<Result> UpdateAsync(CalendarRequestDto requestDto)
    {
        var calendar = await _dbContext.Calendars
            .FirstOrDefaultAsync(x => x.Id.Equals(requestDto.Id));

        if (calendar is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        _mapper.Map(requestDto, calendar);

        var result = await _dbContext.SaveChangesAsync();

        if (result > 0)
        {
            return new SuccessResult(UiMessages.Success);
        }

        return new ErrorResult(UiMessages.Error);
    }

    public async Task<DataResult<List<CalendarResponseDto>>> ListAsync()
    {
        var calendars = await _dbContext.Calendars
            .AsNoTracking()
            .Where(x => x.SiteId.Equals(_currentSiteId))
            .ToListAsync();

        var mappedData = _mapper.Map<List<CalendarResponseDto>>(calendars);

        return new SuccessDataResult<List<CalendarResponseDto>>(mappedData, UiMessages.Success);
    }

    public async Task<DataResult<CalendarResponseDto>> GetAsync(int id)
    {
        var calendar = await _dbContext.Calendars
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (calendar is null)
        {
            return new ErrorDataResult<CalendarResponseDto>(UiMessages.NotFoundData);
        }

        var mappedData = _mapper.Map<CalendarResponseDto>(calendar);

        return new SuccessDataResult<CalendarResponseDto>(mappedData, UiMessages.Success);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var calendar = await _dbContext.Calendars
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (calendar is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        _dbContext.Calendars.Remove(calendar);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }
}