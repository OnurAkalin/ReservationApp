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

    public async Task<Result> InsertAsync(ReservationRequestDto requestDto)
    {
        var reservation = _mapper.Map<Reservation>(requestDto);
        reservation.CreateDate = DateTime.Now;
        reservation.CreateUser = _currentUserId;
        reservation.SiteId = _currentSiteId;
        reservation.UserId = requestDto.Meta.UserId ?? _currentUserId;

        await _dbContext.Reservations.AddAsync(reservation);
        var affectedRowCount = await _dbContext.SaveChangesAsync();

        return affectedRowCount > 0 
            ?  new SuccessResult(UiMessages.Success) 
            :  new ErrorResult(UiMessages.Error);
    }

    public async Task<Result> UpdateAsync(ReservationRequestDto requestDto)
    {
        var reservation = await _dbContext.Reservations
            .FirstOrDefaultAsync(x => x.Id.Equals(requestDto.Id));

        if (reservation is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        _mapper.Map(requestDto, reservation);
        var affectedRowCount = await _dbContext.SaveChangesAsync();

        return affectedRowCount > 0 
            ?  new SuccessResult(UiMessages.Success) 
            :  new ErrorResult(UiMessages.Error);
    }

    public async Task<DataResult<List<ReservationResponseDto>>> ListAsync()
    {
        var reservations = await _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.SiteService)
            .Where(x => x.SiteId.Equals(_currentSiteId))
            .ToListAsync();

        var mappedData = _mapper.Map<List<ReservationResponseDto>>(reservations);

        return new SuccessDataResult<List<ReservationResponseDto>>(mappedData, UiMessages.Success);
    }

    public async Task<DataResult<ReservationResponseDto>> GetAsync(int id)
    {
        var reservation = await _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.SiteService)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (reservation is null)
        {
            return new ErrorDataResult<ReservationResponseDto>(UiMessages.NotFoundData);
        }

        var mappedData = _mapper.Map<ReservationResponseDto>(reservation);

        return new SuccessDataResult<ReservationResponseDto>(mappedData, UiMessages.Success);
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