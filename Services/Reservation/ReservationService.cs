namespace Services;

public class ReservationService : BasicService, IReservationService
{
    private readonly UserManager<User> _userManager;
    public ReservationService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        UserManager<User> userManager
    )
        : base(logger, mapper, dbContext, httpContextAccessor)
    {
        _userManager = userManager;
    }

    public async Task<Result> InsertAsync(ReservationRequestDto requestDto)
    {
        var reservation = _mapper.Map<Reservation>(requestDto);
        reservation.CreateDate = DateTime.Now;
        reservation.CreateUser = _currentUserId;
        reservation.SiteId = _currentSiteId;
        reservation.UserId = requestDto.Meta.UserId ?? _currentUserId;

        await _dbContext.Reservations.AddAsync(reservation);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
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

        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
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

        var user = await _userManager.FindByIdAsync(_currentUserId.ToString());
        var isCustomer = await _userManager.IsInRoleAsync(user, UserRoles.Customer);
        
        if (isCustomer)
        {
            foreach (var reservationResponseDto in mappedData)
            {
                reservationResponseDto.Draggable = false;
                reservationResponseDto.Actions.Deletable = false;
                reservationResponseDto.Actions.Editable = false;
                reservationResponseDto.Resizable.AfterEnd = false;
                reservationResponseDto.Resizable.BeforeStart = false;
            }
        }
        
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
        
        var user = await _userManager.FindByIdAsync(_currentUserId.ToString());
        var isCustomer = await _userManager.IsInRoleAsync(user, UserRoles.Customer);

        if (isCustomer)
        {
            mappedData.Draggable = false;
            mappedData.Actions.Deletable = false;
            mappedData.Actions.Editable = false;
            mappedData.Resizable.AfterEnd = false;
            mappedData.Resizable.BeforeStart = false;
        }

        return new SuccessDataResult<ReservationResponseDto>(mappedData, UiMessages.Success);
    }

    public async Task<Result> CancelAsync(int id)
    {
        var reservation = await _dbContext.Reservations
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (reservation is null)
        {
            return new ErrorResult(UiMessages.NotFoundData);
        }

        reservation.IsCancelled = true;
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }
}