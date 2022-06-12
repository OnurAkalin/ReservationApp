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
            .Include(x => x.SiteService)
            .Where(x => x.SiteId.Equals(_currentSiteId)
                        && x.Start >= startDate
                        && x.End <= endDate)
            .ToListAsync();

        var siteServices = await _dbContext.SiteServices
            .AsNoTracking()
            .Where(x => x.SiteId.Equals(_currentSiteId))
            .ToListAsync();

        var customers = await (from user in _dbContext.Users
                join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
                join role in _dbContext.Roles on userRole.RoleId equals role.Id
                where role.Name.Equals(UserRoles.Customer) && user.SiteId.Equals(_currentSiteId)
                select user)
            .ToListAsync();

        var completedReservationTotal = reservations.Count(x => !x.IsCancelled);
        var cancelledReservationTotal = reservations.Count(x => x.IsCancelled);
        var siteServiceTotal = siteServices.Count;
        var userTotal = customers.Count;
        var maleTotal = customers.Count(x => x.Gender == Gender.Male);
        var femaleTotal = customers.Count(x => x.Gender == Gender.Female);

        var groupedBySiteService = reservations.GroupBy(x => x.SiteService.Name);

        var siteServiceSummary = groupedBySiteService
            .Select(x =>
                new WeeklySummarySiteServiceResponseDto
                {
                    SiteServiceName = x.Key,
                    Total = x.Count()
                }
            ).ToList();

        var groupedByDay = reservations.GroupBy(x => x.Start.DayOfWeek);

        var daySummary = groupedByDay
            .Select(x => new WeeklySummaryDayResponseDto
            {
                Day = x.Key,
                CompletedTotal = x.Count(y => !y.IsCancelled),
                CancelledTotal = x.Count(y => y.IsCancelled)
            }).ToList();
        
        var responseDto = new WeeklySummaryResponseDto
        {
            CompletedReservationTotal = completedReservationTotal,
            CancelledReservationTotal = cancelledReservationTotal,
            SiteServiceTotal = siteServiceTotal,
            UserTotal = userTotal,
            MaleTotal = maleTotal,
            FemaleTotal = femaleTotal,
            DaySummary = daySummary,
            SiteServiceSummary = siteServiceSummary
        };

        return new SuccessDataResult<WeeklySummaryResponseDto>(responseDto, UiMessages.Success);
    }

    public async Task<DataResult<List<MonthlySummaryResponseDto>>> GetMonthlyUserSummaryAsync()
    {
        var customers = await (from user in _dbContext.Users
                join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
                join role in _dbContext.Roles on userRole.RoleId equals role.Id
                where role.Name.Equals(UserRoles.Customer) && user.SiteId.Equals(_currentSiteId)
                select user)
            .ToListAsync();

        var groupedByMonth = customers.GroupBy(x => x.CreateDate.Month);

        var monthlyUserSummary = groupedByMonth
            .Select(x => new MonthlySummaryResponseDto
            {
                Month = x.Key,
                Total = x.Count()
            }).ToList();

        return new SuccessDataResult<List<MonthlySummaryResponseDto>>(monthlyUserSummary, UiMessages.Success);
    }

    public async Task<DataResult<List<MonthlySummaryResponseDto>>> GetMonthlyIncomeSummaryAsync()
    {
        var reservations = await _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.SiteService)
            .Where(x => x.SiteId.Equals(_currentSiteId))
            .ToListAsync();

        var groupedByMonth = reservations.GroupBy(x => x.Start.Month);
        
        var monthlyIncomeSummary = groupedByMonth
            .Select(x => new MonthlySummaryResponseDto
            {
                Month = x.Key,
                Total = x.Sum(y => y.SiteService.Price) ?? default
            }).ToList();

        return new SuccessDataResult<List<MonthlySummaryResponseDto>>(monthlyIncomeSummary, UiMessages.Success);
        
    }

    public async Task<DataResult<List<MonthlySummaryResponseDto>>> GetMonthlyReservationSummaryAsync()
    {
        var reservations = await _dbContext.Reservations
            .AsNoTracking()
            .Where(x => x.SiteId.Equals(_currentSiteId))
            .ToListAsync();

        var groupedByMonth = reservations.GroupBy(x => x.Start.Month);
        
        var monthlyReservationSummary = groupedByMonth
            .Select(x => new MonthlySummaryResponseDto
            {
                Month = x.Key,
                Total = x.Count()
            }).ToList();

        return new SuccessDataResult<List<MonthlySummaryResponseDto>>(monthlyReservationSummary, UiMessages.Success);
    }
}