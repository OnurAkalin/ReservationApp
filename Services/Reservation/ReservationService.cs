namespace Services;

public class ReservationService : BasicService, IReservationService
{
    public ReservationService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor
    )
        : base(logger, mapper, dbContext, httpContextAccessor)
    {
    }

    public async Task<Result> InsertAsync(ReservationMainDto requestDto)
    {
        var reservation = _mapper.Map<Reservation>(requestDto);
        reservation.CreateDate = DateTime.Now;
        reservation.CreateUser = _currentUserId;
        reservation.SiteId = _currentSiteId;
        reservation.UserId = requestDto.Meta.UserId ?? _currentUserId;

        await _dbContext.Reservations.AddAsync(reservation);
        var result = await _dbContext.SaveChangesAsync();

        if (result > 0)
        {
            return new SuccessResult(UiMessages.Success);
        }

        return new ErrorResult(UiMessages.Error);
    }

    public async Task<Result> UpdateAsync(ReservationMainDto requestDto)
    {
        var reservation = await _dbContext.Reservations
            .FirstOrDefaultAsync(x => x.Id.Equals(requestDto.Id));

        if (reservation is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        _mapper.Map(requestDto, reservation);

        var result = await _dbContext.SaveChangesAsync();

        if (result > 0)
        {
            return new SuccessResult(UiMessages.Success);
        }

        return new ErrorResult(UiMessages.Error);
    }

    public async Task<DataResult<List<ReservationMainDto>>> ListAsync()
    {
        var reservations = await _dbContext.Reservations
            .AsNoTracking()
            .Where(x => x.SiteId.Equals(_currentSiteId))
            .ToListAsync();

        var mappedData = _mapper.Map<List<ReservationMainDto>>(reservations);

        return new SuccessDataResult<List<ReservationMainDto>>(mappedData, UiMessages.Success);
    }

    public async Task<DataResult<ReservationMainDto>> GetAsync(int id)
    {
        var reservation = await _dbContext.Reservations
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (reservation is null)
        {
            return new ErrorDataResult<ReservationMainDto>(UiMessages.NotFoundData);
        }

        var mappedData = _mapper.Map<ReservationMainDto>(reservation);

        return new SuccessDataResult<ReservationMainDto>(mappedData, UiMessages.Success);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var reservation = await _dbContext.Reservations
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (reservation is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        _dbContext.Reservations.Remove(reservation);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }
}