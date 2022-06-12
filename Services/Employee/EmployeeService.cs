namespace Services;

public class EmployeeService : BasicService, IEmployeeService
{
    private readonly UserManager<User> _userManager;

    public EmployeeService
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

    public async Task<DataResult<List<EmployeeResponseDto>>> ListAsync()
    {
        var employees = await (from user in _dbContext.Users
            join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
            join role in _dbContext.Roles on userRole.RoleId equals role.Id
            where role.Name.Equals(UserRoles.Employee) && user.SiteId.Equals(_currentSiteId)
            select new EmployeeResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                LastLoginDate = user.LastLoginDate
            }).ToListAsync();

        return new SuccessDataResult<List<EmployeeResponseDto>>(employees, UiMessages.Success);
    }

    public async Task<Result> InsertAsync(EmployeeRequestDto requestDto)
    {
        var site = await _dbContext.Sites
            .FirstOrDefaultAsync(x => x.Id.Equals(_currentSiteId));

        if (site is null)
        {
            return new ErrorResult(UiMessages.UnselectedSite);
        }

        var userName = site.Id + "_" + requestDto.Email;

        var checkUserExist = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.UserName.Equals(userName));

        if (checkUserExist)
        {
            return new ErrorResult(UiMessages.UserAlreadyExist);
        }

        var user = _mapper.Map<User>(requestDto);
        user.UserName = userName;
        user.SiteId = site.Id;
        user.CreateDate = DateTime.Now;

        var createUserResult = await _userManager.CreateAsync(user, requestDto.Password);

        if (!createUserResult.Succeeded)
        {
            return new ErrorResult(UiMessages.InvalidCredentials);
        }

        await _userManager.AddToRoleAsync(user, UserRoles.Employee);

        return new SuccessResult(UiMessages.Success);
    }

    public async Task<DataResult<EmployeeResponseDto>> GetAsync(int id)
    {
        var employee = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (employee is null)
        {
            return new ErrorDataResult<EmployeeResponseDto>(UiMessages.UserNotFound);
        }

        var mappedData = _mapper.Map<EmployeeResponseDto>(employee);

        return new SuccessDataResult<EmployeeResponseDto>(mappedData, UiMessages.Success);
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var employee = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (employee is null)
        {
            return new ErrorResult(UiMessages.UserNotFound);
        }

        _dbContext.Remove(employee);
        await _dbContext.SaveChangesAsync();

        return new SuccessResult(UiMessages.Success);
    }
}