using Domain.Entities.Dashboard;

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

    public async Task<DataResult<int>> CalculateIncome(CalculateIncomeRequestDto requestDto)
    {
        var reservationTotal = await _dbContext.Reservations
            .AsNoTracking()
            .Include(x => x.SiteService)
            .Where(x => x.SiteId.Equals(_currentSiteId)&& x.Start >= requestDto.StartDate
                        && x.End <= requestDto.EndDate)
            .SumAsync(x => x.SiteService.Price);

        return !reservationTotal.HasValue 
            ? new SuccessDataResult<int>(default, UiMessages.Success) 
            : new SuccessDataResult<int>(reservationTotal.Value, UiMessages.Success);
    }
}