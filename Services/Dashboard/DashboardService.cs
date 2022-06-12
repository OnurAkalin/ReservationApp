namespace Services;

public class DashboardService : BasicService, IDashboardService
{
    public DashboardService
    (
        Logger logger,
        IMapper mapper,
        ApplicationDbContext dbContext,
        IHttpContextAccessor httpContextAccessor
    )
        : base(logger, mapper, dbContext, httpContextAccessor)
    {
    }

    public async Task<DataResult<List<UserResponseDto>>> GetLastActiveUsersAsync()
    {
        var customers = await (from user in _dbContext.Users
                join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
                join role in _dbContext.Roles on userRole.RoleId equals role.Id
                where role.Name.Equals(UserRoles.Customer) && user.SiteId.Equals(_currentSiteId)
                select new UserResponseDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    LastLoginDate = user.LastLoginDate
                }).OrderByDescending(x => x.LastLoginDate)
            .Take(10)
            .ToListAsync();

        return new SuccessDataResult<List<UserResponseDto>>(customers, UiMessages.Success);
    }

    public async Task<DataResult<int>> CalculateIncomeAsync(CalculateIncomeRequestDto requestDto)
    {
        var reservationTotal = await _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.SiteService)
            .Where(x => x.SiteId.Equals(_currentSiteId) 
                        && x.Start >= requestDto.StartDate
                        && x.End <= requestDto.EndDate)
            .SumAsync(x => x.SiteService.Price);

        return !reservationTotal.HasValue
            ? new SuccessDataResult<int>(default, UiMessages.Success)
            : new SuccessDataResult<int>(reservationTotal.Value, UiMessages.Success);
    }

    public async Task<DataResult<WeeklySummaryResponseDto>> GetWeeklySummaryAsync()
    {
        var startDate = DateTime.Now.AddDays(-7);
        var endDate = DateTime.Now;

        var reservations = await _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.SiteService)
            .Where(x => x.SiteId.Equals(_currentSiteId)
                        && x.Start >= startDate
                        && x.End <= endDate)
            .ToListAsync();

        var siteServices = await _dbContext.SiteServices
            .AsNoTracking()
            .Where(x => x.SiteId.Equals(_currentSiteId))
            .ToListAsync();

        var users = await _dbContext.Users
            .AsNoTracking()
            .Where(x => x.SiteId.Equals(_currentSiteId)
                        && x.CreateDate >= startDate
                        && x.CreateDate <= endDate)
            .ToListAsync();

        var completedReservationTotal = reservations.Count(x => !x.IsCancelled);
        var cancelledReservationTotal = reservations.Count(x => x.IsCancelled);
        var siteServiceTotal = siteServices.Count;
        var userTotal = users.Count;
        var maleTotal = users.Count(x => x.Gender == Gender.Male);
        var femaleTotal = users.Count(x => x.Gender == Gender.Female);

        var groupedReservations = reservations.GroupBy(x => x.SiteService.Name);

        foreach (var key in groupedReservations)
        {
            foreach (var reservation in key)
            {
                Console.WriteLine(reservation.SiteService.Name);
            }
        }


        var responseDto = new WeeklySummaryResponseDto
        {
            CompletedReservationTotal = completedReservationTotal,
            CancelledReservationTotal = cancelledReservationTotal,
            SiteServiceTotal = siteServiceTotal,
            UserTotal = userTotal,
            MaleTotal = maleTotal,
            FemaleTotal = femaleTotal
        };

        return new SuccessDataResult<WeeklySummaryResponseDto>(responseDto, UiMessages.Success);
    }
}